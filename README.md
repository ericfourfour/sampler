sampler
=======

C# Sound Sampler for WinRT (Windows 8.1, Windows Phone 8.1)

Usage:

```
// Create a sampler that will load all of the sound files with the "wav" extension in the "sounds" folder.
var sampler = new XAudio2Sampler("sounds", "wav");

// Play the file at "sounds/click.wav" after the sampler is loaded.
sampler.Loaded += (sender, e) => { sampler.Play("click"); }

// Load all of the files into the sampler.
sampler.Load();
```


Implementing Custom Samplers:

This section is to be completed. For now, look at the source. It's not overly-complicated. You will need to 
inherit from the Sampler and Sound abstract classes and implement their abstract methods.
