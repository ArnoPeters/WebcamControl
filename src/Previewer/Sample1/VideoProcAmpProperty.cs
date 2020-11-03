using System;

// See CppSDK\SDK\include\shared\ksmedia.h
//    typedef enum {
//        KSPROPERTY_VIDEOPROCAMP_BRIGHTNESS,                 // RW O
//        KSPROPERTY_VIDEOPROCAMP_CONTRAST,                   // RW O
//        KSPROPERTY_VIDEOPROCAMP_HUE,                        // RW O
//        KSPROPERTY_VIDEOPROCAMP_SATURATION,                 // RW O
//        KSPROPERTY_VIDEOPROCAMP_SHARPNESS,                  // RW O
//        KSPROPERTY_VIDEOPROCAMP_GAMMA,                      // RW O
//        KSPROPERTY_VIDEOPROCAMP_COLORENABLE,                // RW O
//        KSPROPERTY_VIDEOPROCAMP_WHITEBALANCE,               // RW O
//        KSPROPERTY_VIDEOPROCAMP_BACKLIGHT_COMPENSATION      // RW O
//        KSPROPERTY_VIDEOPROCAMP_GAIN                        // RW O
//    }
//    KSPROPERTY_VIDCAP_VIDEOPROCAMP;

namespace MFCaptureD3D.Sample1
{
  /// <summary>
  /// The list of video camera settings
  /// </summary>
  /// <remarks>Code copied from https://gist.github.com/MZachmann/557bf6663ce4806dfa85f1cd348027b4</remarks>
  public enum VideoProcAmpProperty
        {
            Brightness = 0,
            Contrast,
            Hue,
            Saturation,
            Sharpness,
            Gamma,
            ColorEnable,
            WhiteBalance,
            BacklightCompensation,
            Gain
        }

  /// <summary>
  /// The auto and manual flag
  /// </summary>
  /// <remarks>Code copied from https://gist.github.com/MZachmann/557bf6663ce4806dfa85f1cd348027b4</remarks>
  [Flags]
    public enum VideoProcAmpFlags
        {
            None = 0x0,
            Auto = 0x0001,
            Manual = 0x0002
        }
}
