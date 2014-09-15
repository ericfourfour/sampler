using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace quiz.sampler
{
    /// <summary>
    /// The base class for all sampler-sounds.
    /// </summary>
    public abstract class Sound
    {
        #region Constructors
        /// <summary>
        /// Create a new sound from the specified StorageFile.
        /// </summary>
        /// <param name="file">The file containing the sound data.</param>
        public Sound(StorageFile file)
        {
            File = file;
        }
        #endregion

        #region Controls
        /// <summary>
        /// Load this sound from its StorageFile.
        /// </summary>
        public abstract void Load();
        /// <summary>
        /// Play this sound.
        /// </summary>
        public abstract void Play();
        /// <summary>
        /// Stop this sound.
        /// </summary>
        public abstract void Stop();
        #endregion

        #region Properties
        /// <summary>
        /// The file containing this sound's data.
        /// </summary>
        public StorageFile File { get { return m_file; } set { m_file = value; } }
        #endregion

        #region Fields
        /// <summary>
        /// The file containing this sound's data. This field is linked to the
        /// File property.
        /// </summary>
        private StorageFile m_file;
        #endregion

        #region Events
        /// <summary>
        /// Intercept this event to run code upon this sound being loaded.
        /// </summary>
        public event EventHandler Loaded;
        #endregion

        #region Event Triggers
        /// <summary>
        /// Call this method to signal that the sound is done loading.
        /// </summary>
        protected virtual void OnLoaded() { Loaded(this, new EventArgs()); }
        #endregion
    }
}
