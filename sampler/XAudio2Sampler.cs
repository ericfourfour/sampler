using SharpDX.XAudio2;
using System;
using Windows.Storage;

namespace sampler
{
    /// <summary>
    /// A sampler implementation that uses ShardDX.XAudio2 to render audio.
    /// </summary>
    public class XAudio2Sampler : Sampler<XAudio2Sound>
    { 
        #region Constructors
        /// <summary>
        /// Create a new XAudio2Sampler that loads all of the audio files in
        /// the specified folder with the specified extension.
        /// </summary>
        /// <param name="folderName">The folder containing the audio files.</param>
        /// <param name="extension">The extension of the audio files.</param>
        public XAudio2Sampler(String folderName, String extension) : base(folderName, extension)
        {
            m_xaudio = new XAudio2();
            m_masteringVoice = new MasteringVoice(m_xaudio);
        }
        #endregion

        #region Sampler Overrides
        /// <summary>
        /// Create a new XAudio2Sound.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected override XAudio2Sound CreateNewSound(StorageFile file)
        {
            return new XAudio2Sound(file, m_xaudio);
        }
        #endregion

        #region Fields
        /// <summary>
        /// The XAudio2 engine that is responsible for audio processing.
        /// </summary>
        private XAudio2 m_xaudio;
        /// <summary>
        /// The mastering voice of the XAudio2 engine.
        /// </summary>
        private MasteringVoice m_masteringVoice;
        #endregion
    }
}
