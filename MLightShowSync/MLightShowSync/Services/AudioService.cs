using Spectrogram;

namespace MLightShowSync.Services
{
    public class AudioService
    {
        (double[] audio, int sampleRate) ReadMono(string filePath, double multiplier = 16_000)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int bytesPerSample = afr.WaveFormat.BitsPerSample / 8;
            int sampleCount = (int)(afr.Length / bytesPerSample);
            int channelCount = afr.WaveFormat.Channels;

            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;

            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));

            return (audio.ToArray(), sampleRate);
        }

        public Stream CreateSpectrogram(string filePath, int width)
        {
            var specFile = $"{filePath}.spec.{width}.png";

            if (!File.Exists(specFile))
            {
                (double[] audio, int sampleRate) = ReadMono(filePath);

                int fftSize = 16384;          
                int stepSize = audio.Length / width;

                var sg = new SpectrogramGenerator(sampleRate, fftSize, stepSize, maxFreq: 2200);
                sg.Add(audio);

                sg.SaveImage(specFile, intensity: 5, dB: true);
            }

            return File.OpenRead(specFile);
        }

        public double GetSongLength(string filePath)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            return afr.TotalTime.TotalSeconds;
        }

    }
}
