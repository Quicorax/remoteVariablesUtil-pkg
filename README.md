Jam Utility Bundle Package

## Installation using the Unity Package Manager
1. Open the Package Manager Window. 
2. Click on the + icon on the top left.
3. Select "Add package from git URL...".
4. In the input field enter the URL of the git repository ("https://github.com/Quicorax/jamUtils-pkg.git")
5. Click on "Add" and wait until the package is downloaded and imported.

# AUDIO
Audio management utility for Unity.

## SetUp
The utility needs to be initialized, to do so: 
- Go to Packages -> Jam Utilities Package -> Sample. Then drop the "ServiceLoader" prefab to the scene. (NOTE: this MonoBehaviour should only be initialized once on the whole application flow)
Generate a new "HardAudioDefinitions" by:
- Inside any Assets folder. Left mouse click -> Create -> Quicorax -> AudioUtil -> HardAudioDefinitions.

Set the reference of the newly created HardAudioDefinitions to the previously added AudioInitializer.

Fill the HardAudioDefinitions with your audio clips and their associated keys.

## Usage
All the package calls are accessed by calling the "AudioPlayer" service from the ServiceLocator.

There are the following calls:
```
PlaySFX(string clipKey);
PlayMusic(string clipKey);
StopMusic(string clipKey, float fadeTime = 0, Action onComplete = null);
StopAllMusics(float fadeTime);
Clear();

AddMasterVolume(float additiveValue);
AddMusicVolume(float additiveValue);
AddSFXVolume(float additiveValue);
MuteMaster();
MuteMusic();
MuteSFX();
```
## Example
``` 
var audioService = ServiceLocator.GetService<AudioService>();
audioService.StopMusic("CaveMusic", 2, ()=> audioService.PlayMusic("CaveBossMusic"));
```

## Dependencies
DOTween: (https://dotween.demigiant.com/).


# Localization
TBD

# Remote Assets
TBD

## by: @quicorax
Under [MIT copyright license](https://github.com/Quicorax/jamUtils-pkg/blob/main/LICENSE.txt).

--- @quicorax ---
