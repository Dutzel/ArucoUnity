﻿using ArucoUnity.Cameras.Parameters;
using ArucoUnity.Plugin;
using System;

namespace ArucoUnity
{
  /// \addtogroup aruco_unity_package
  /// \{

  namespace Cameras.Undistortions
  {
    /// <summary>
    /// Manages the undistortion and rectification process for fisheye and omnidir <see cref="StereoArucoCamera"/>.
    /// </summary>
    public class StereoOmnidirCameraUndistortion : OmnidirCameraUndistortionGeneric<StereoArucoCamera>
    {
      // ArucoCameraController methods

      public override void Configure()
      {
        if (CameraParameters.StereoCameraParameters == null)
        {
          throw new Exception("The camera parameters must contains a valid StereoCameraParameters to undistort and rectify a StereoArucoCamera.");
        }
        base.Configure();
      }

      // ArucoCameraUndistortion methods

      /// <summary>
      /// Initializes <see cref="RectificationMatrices"/> with the stereo camera parameters.
      /// </summary>
      protected override void InitializeRectification()
      {
        base.InitializeRectification();

        Cv.Mat rectificationMatrix1, rectificationMatrix2;
        Cv.Omnidir.StereoRectify(CameraParameters.StereoCameraParameters.RotationVector, CameraParameters.StereoCameraParameters.TranslationVector,
          out rectificationMatrix1, out rectificationMatrix2);

        RectificationMatrices[StereoArucoCamera.CameraId1] = rectificationMatrix1;
        RectificationMatrices[StereoArucoCamera.CameraId2] = rectificationMatrix2;
      }
    }
  }

  /// \} aruco_unity_package
}