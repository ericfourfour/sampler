using SharpDX.IO;
using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace quiz.sampler
{
    /// <summary>
    /// A sampler-sound implementation that uses ShardDX.XAudio2 to render audio.
    /// </summary>
    public class XAudio2Sound : Sound
    {
        #region Constructors
        /// <summary>
        /// Create a new XAudio2Sound from a specified StarageFile and XAudio2
        /// engine.
        /// </summary>
        /// <param name="file">The file containing the sound-data.</param>
        /// <param name="xaudio">The XAudio2 engine.</param>
        public XAudio2Sound(StorageFile file, XAudio2 xaudio) : base(file)
        {
            XAudio = xaudio;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The XAudio2 engine that does all of the audio processing for this
        /// sound implementation.
        /// </summary>
        public XAudio2 XAudio
        {
            get { return m_xaudio; }
            set { m_xaudio = value; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// The XAudio2 engine that does the audio processing for this Sound
        /// implementation. This field is linked to the XAudio property.
        /// </summary>
        private XAudio2 m_xaudio;
        /// <summary>
        /// The stream containing the sound-data.
        /// </summary>
        private SoundStream m_soundStream;
        /// <summary>
        /// The format of the wave audio data.
        /// </summary>
        private WaveFormat m_waveFormat;
        /// <summary>
        /// The buffer into which sound data is to be loaded and submitted to
        /// the XAudio engine.
        /// </summary>
        private AudioBuffer m_buffer;
        #endregion

        #region Sound Overrides
        /// <summary>
        /// Load the sound data from the sound-file.
        /// </summary>
        public override void Load()
        {
            var nativeFileStream = new NativeFileStream(
                File.Path,
                NativeFileMode.Open,
                NativeFileAccess.Read,
                NativeFileShare.Read);

            m_soundStream = new SoundStream(nativeFileStream);
            m_waveFormat = m_soundStream.Format;
            m_buffer = new AudioBuffer
            {
                Stream = m_soundStream.ToDataStream(),
                AudioBytes = (int)m_soundStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            OnLoaded();
        }
        /// <summary>
        /// Play the sound.
        /// </summary>
        public override void Play()
        {
            SourceVoice sourceVoice;
            sourceVoice = new SourceVoice(m_xaudio, m_waveFormat, true);
            sourceVoice.SubmitSourceBuffer(m_buffer, m_soundStream.DecodedPacketsInfo);
            sourceVoice.Start();
        }
        /// <summary>
        /// Stop the sound.
        /// </summary>
        public override void Stop()
        {
            
        }
        #endregion
    }
}
