using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Storage;

namespace sampler
{
    /// <summary>
    /// The base class for all samplers.
    /// </summary>
    /// <typeparam name="T">The sampler-sound type.</typeparam>
    public abstract class Sampler<T> where T : Sound
    {
        #region Constructors
        /// <summary>
        /// Create a new Sampler for all of the files with the specified
        /// extension in the specifeid folder.
        /// </summary>
        /// <param name="folderName">The folder containing audio files.</param>
        /// <param name="extension">The extension of the audio files.</param>
        public Sampler(String folderName, String extension)
        {
            Sounds = new Dictionary<String, T>();
            FolderName = folderName;
            Extension = extension;
        }
        #endregion

        #region Controls
        /// <summary>
        /// Load all of the sound files. When completed, trigger the Loaded event.
        /// </summary>
        public async void Load()
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync(FolderName);
            var files = await folder.GetFilesAsync();
            foreach (var file in files) LoadSound(file);

            OnLoaded();
        }
        /// <summary>
        /// Load a single sound file. When completed, trigger the SoundLoaded event.
        /// </summary>
        /// <param name="file">The file containing the sound file to load.</param>
        public void LoadSound(StorageFile file)
        {
            if (file.Name.EndsWith("." + Extension))
            {
                var sound = CreateNewSound(file);
                var soundName = file.Name.Substring(0, file.Name.Length - Extension.Length - 1);

                Sounds[soundName] = sound;
                sound.Loaded += (sender, e) => { OnSoundLoaded(sound); };
                sound.Load();
            }
        }
        #endregion

        #region Sound Helpers
        /// <summary>
        /// Play the sound file under the specified name.
        /// </summary>
        /// <param name="name">The name of the sound file.</param>
        public void Play(String name)
        {
            Sounds[name].Play();
        }
        /// <summary>
        /// Stop the sound file under the specified name.
        /// </summary>
        /// <param name="name"></param>
        public void Stop(String name)
        {
            Sounds[name].Stop();
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Creates a new Sound object given a specified StorageFile.
        /// 
        /// Override this file for the load functionality to work.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected abstract T CreateNewSound(StorageFile file);
        #endregion

        #region Properties
        /// <summary>
        /// All of the sounds in this sampler.
        /// </summary>
        public Dictionary<String, T> Sounds 
        {
            get { return m_sounds; }
            set { m_sounds = value; }
        }
        /// <summary>
        /// The folder-name of the sounds in this sampler.
        /// </summary>
        public String FolderName
        {
            get { return m_folderName; }
            set { m_folderName = value; }
        }
        /// <summary>
        /// The extension of the sound-files in this sample.
        /// </summary>
        public String Extension
        {
            get { return m_extension; }
            set { m_extension = value; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// All of the sounds in this sampler. This field is linked to the
        /// Sounds property.
        /// </summary>
        private Dictionary<String, T> m_sounds;
        /// <summary>
        /// The name of the folder containing all of the sound files for this
        /// sampler. This field is linked to the FolderName property.
        /// </summary>
        private String m_folderName;
        /// <summary>
        /// The extension of all of the sound files for this sampler. This
        /// field is linked to the Extension property.
        /// </summary>
        private String m_extension;
        #endregion

        #region Events
        /// <summary>
        /// Intercept this event to run code upon this sampler being loaded.
        /// </summary>
        public event EventHandler Loaded;
        /// <summary>
        /// Intercept this event to run code upun a sound being loaded in this sampler.
        /// </summary>
        public event EventHandler<SoundLoadedEventArgs<T>> SoundLoaded;
        #endregion

        #region Event Triggers
        /// <summary>
        /// Call this function immediately after loading this sampler to trigger the Loaded event handler.
        /// </summary>
        protected virtual void OnLoaded() { if (Loaded != null) Loaded(this, new EventArgs()); }
        /// <summary>
        /// Call this function immediately after loading a sound to trigger the SoundLoaded event handler.
        /// </summary>
        /// <param name="sound">The sound that was loaded.</param>
        protected virtual void OnSoundLoaded(T sound) { if (SoundLoaded != null) SoundLoaded(this, new SoundLoadedEventArgs<T>(sound)); }
        #endregion
    }

    #region EventArgs
    public class SoundLoadedEventArgs<T> : EventArgs where T : Sound
    {
        public SoundLoadedEventArgs(T sound) { Sound = sound; }
        public T Sound { get; set; }
    }
    #endregion
}
