using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// Note: This class is written for Unity 2018.x Versions of Unity and is ported for older versions for historical reasons.
// The script will not work a 100% for Versions older than 2017.2, but it will compile and will not throw errors for Unity 5.x.
namespace DeepSpace.Editor
{
	public class DeepSpaceSettingsEditor : UnityEditor.EditorWindow
	{
		private bool _vrSdkFoldout = true;
		private const string _vrSdkStereo = "stereo";
		private bool _graphicsApiFoldout = true;
		private const UnityEngine.Rendering.GraphicsDeviceType _graphicsDeviceGLCore = UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore;
		private const UnityEngine.Rendering.GraphicsDeviceType _graphicsDeviceDx11 = UnityEngine.Rendering.GraphicsDeviceType.Direct3D11;

		private Vector2 _scrollPosition;

		private float _leftLabelMaxWidth = 270f;

		private bool _everythingSet = false;

		private readonly string _axisVerticalLeft = "DS_VerticalLeft";
		private readonly string _axisHorizontalLeft = "DS_HorizontalLeft";
		private readonly string _axisVerticalRight = "DS_VerticalRight";
		private readonly string _axisHorizontalRight = "DS_HorizontalRight";
		private readonly string _axisDPadVertical = "DS_DPadVertical";
		private readonly string _axisDPadHorizontal = "DS_DPadHorizontal";
		private readonly string _axisShoulderTriggerL2 = "DS_ShoulderTriggerL2";
		private readonly string _axisShoulderTriggerR2 = "DS_ShoulderTriggerR2";

		// Add menu named "Settings Window" to the DeepSpace menu
		[MenuItem("DeepSpace/Settings...")]
		public static void Init()
		{
			// Get existing open window or if it is not yet existing, create a new one:
			DeepSpaceSettingsEditor window = EditorWindow.GetWindow(typeof(DeepSpaceSettingsEditor), true, "Deep Space Settings") as DeepSpaceSettingsEditor;
			window.Show();
		}

		private void OnGUI()
		{
			// Headline:
			EditorGUILayout.LabelField("Deep Space Settings", EditorStyles.centeredGreyMiniLabel);

			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

			// Fast lane:
			//if (_everythingSet == false)
			//{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Fast lane", EditorStyles.boldLabel);

			EditorGUI.indentLevel++; // Start Intend Level Fast Lane

			ManageApplyAllRecommendedSettings();

			EditorGUI.indentLevel--; // End Intend Level Fast Lane
									 //}

			// Player Settings:
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Player Settings", EditorStyles.boldLabel);

			EditorGUI.indentLevel++; // Start Intend Level Player Settings

			_everythingSet = true; // This will be set false by and-assignment, if a Manage* method is not completely satisfied.

			// Virtual Realilty Support:
			_everythingSet &= ManageVirtualRealitySupport();

			// Virtual Reality SDKs:
			_everythingSet &= ManageVirtualRealitySDKs();

			EditorGUILayout.Space();

			// Graphics APIs:
			_everythingSet &= ManageGraphicsAPIs();

			EditorGUILayout.Space();

			// Display Resolution Digalog:
			_everythingSet &= ManageDisplayResolutionDialog();

			// Splash Screen:
			_everythingSet &= ManageSplashScreen();

			EditorGUI.indentLevel--; // End Intend Level Player Settings

			// Input Settings:
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Input Settings", EditorStyles.boldLabel);

			EditorGUI.indentLevel++; // Start Intend Level Input Settings

			_everythingSet &= ManageInputSettings();

			EditorGUI.indentLevel--; // End Intend Level Input Settings

			// Audio Settings:
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Audio Settings", EditorStyles.boldLabel);

			EditorGUI.indentLevel++; // Start Intend Level Audio Settings

			_everythingSet &= ManageAudioSettings();

			EditorGUI.indentLevel--; // End Intend Level Audio Settings

			// Quality Settings:
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Quality Settings", EditorStyles.boldLabel);

			EditorGUI.indentLevel++; // Start Intend Level Audio Settings

			_everythingSet &= ManageQualitySettings();

			EditorGUI.indentLevel--; // End Intend Level Audio Settings

			// End the Window Layout:
			GUILayout.EndScrollView();
		}

		private void ManageApplyAllRecommendedSettings()
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Apply all recommended Settings", GUILayout.MaxWidth(_leftLabelMaxWidth));

			if (_everythingSet == false)
			{
				if (GUILayout.Button("Do the magic"))
				{
					ApplyAllRecommendedSettings();
				}
			}
			else
			{
				EditorGUILayout.LabelField("(All done)");
			}

			EditorGUILayout.EndHorizontal();
		}

		// Returns true, if everything is done:
		private bool ManageVirtualRealitySupport()
		{
			bool allDone = true;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Virtual Reality Supported", "Enable VR Support to create stereo applications."), EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));
			if (PlayerSettings.virtualRealitySupported == false)
			{
				allDone = false;

				if (GUILayout.Button("Enable"))
				{
					EnableVirutalRealitySupport();
				}
			}
			else
			{
				EditorGUILayout.LabelField("(Is enabled)");
			}
			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		private static void EnableVirutalRealitySupport()
		{
			if (PlayerSettings.virtualRealitySupported == false)
			{
#if UNITY_5 || UNITY_2017_1
				PlayerSettings.virtualRealitySupported = true;
#else // #elif UNITY_2017_2_OR_NEWER
				PlayerSettings.SetVirtualRealitySupported(BuildTargetGroup.Standalone, true);
#endif
			}
		}

		// Returns true, if everything is done:
		private bool ManageVirtualRealitySDKs()
		{
			bool allDone = true;

#if UNITY_5 || UNITY_2017_1
			List<string> vrSDKs = new List<string>(UnityEngine.VR.VRSettings.supportedDevices);
#else // #elif UNITY_2017_2_OR_NEWER
			List<string> vrSDKs = new List<string>(PlayerSettings.GetVirtualRealitySDKs(BuildTargetGroup.Standalone));
#endif

			if (vrSDKs.Contains(_vrSdkStereo) == false) // If non head-mounted stereo is not available at all:
			{
				allDone = false;

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Missing non head-mounted stereo", GUILayout.MaxWidth(_leftLabelMaxWidth));

				if (GUILayout.Button("Add"))
				{
					vrSDKs.Insert(0, _vrSdkStereo);

#if UNITY_5 || UNITY_2017_1
					UnityEngine.VR.VRSettings.LoadDeviceByName(_vrSdkStereo);
#else // #elif UNITY_2017_2_OR_NEWER
					PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, vrSDKs.ToArray());
#endif
				}
				EditorGUILayout.EndHorizontal();
			}

			_vrSdkFoldout = EditorGUILayout.Foldout(_vrSdkFoldout, "Virtual Reality SDKs");
			if (_vrSdkFoldout == true)
			{
				if (vrSDKs.Count == 0)
				{
					EditorGUI.indentLevel++; // Start Intend Level VR SDKs
					EditorGUILayout.LabelField("(empty)");
					EditorGUI.indentLevel--; // End Intend Level VR SDKs
				}
				else
				{
					for (int ii = 0; ii < vrSDKs.Count; ++ii)
					{
						string vrSDK = vrSDKs[ii];

						EditorGUILayout.BeginHorizontal();

						EditorGUI.indentLevel++; // Start Intend Level VR SDKs
						EditorGUILayout.LabelField(vrSDK, GUILayout.MaxWidth(_leftLabelMaxWidth));
						EditorGUI.indentLevel--; // End Intend Level VR SDKs

						if (vrSDK == _vrSdkStereo) // If it is the non head-mounted stereo SDK
						{
							if (ii != 0)
							{

#if UNITY_5 || UNITY_2017_1
								GUIStyle yellowTextStyle = new GUIStyle();
								yellowTextStyle.normal.textColor = Color.yellow;
								EditorGUILayout.LabelField("Set SDK to default in Player Settings.", yellowTextStyle);
#else // #elif UNITY_2017_2_OR_NEWER
								allDone = false;

								if (GUILayout.Button("Set as default SDK"))
								{
									// If it is not the first SKD, put it at first in order:
									vrSDKs.RemoveAt(ii);
									vrSDKs.Insert(0, vrSDK);
									PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, vrSDKs.ToArray());
								}
#endif
							}
							else
							{
								EditorGUILayout.LabelField("(Set as default SDK)");
							}
						}
						else // Another SDK, but not stereo
						{
#if UNITY_5 || UNITY_2017_1
							GUIStyle yellowTextStyle = new GUIStyle();
							yellowTextStyle.normal.textColor = Color.yellow;
							EditorGUILayout.LabelField("Remove SDK in Player Settings if not needed.", yellowTextStyle);
#else // #elif UNITY_2017_2_OR_NEWER
							if (GUILayout.Button("Disable"))
							{
								vrSDKs.RemoveAt(ii);
								PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, vrSDKs.ToArray());
							}
#endif
						}
						EditorGUILayout.EndHorizontal();
					}
				}
			}

			return allDone;
		}

		private void SetupVirtualRealitySDKs()
		{
#if UNITY_5 || UNITY_2017_1
			if(System.Array.IndexOf(UnityEngine.VR.VRSettings.supportedDevices, _vrSdkStereo) < 0)
			{
				UnityEngine.VR.VRSettings.LoadDeviceByName(_vrSdkStereo);
			}
#else // #elif UNITY_2017_2_OR_NEWER
			string[] vrSDKs = new string[] { _vrSdkStereo };
			PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, vrSDKs);
#endif
		}

		// Returns true, if everything is done:
		private bool ManageGraphicsAPIs()
		{
			bool allDone = true;

			BuildTarget curBuildTarget = EditorUserBuildSettings.activeBuildTarget;
			EditorGUILayout.LabelField("Current Build Target: " + curBuildTarget);
			if (curBuildTarget != BuildTarget.StandaloneWindows64)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Recommendation is " + BuildTarget.StandaloneWindows64, GUILayout.MaxWidth(_leftLabelMaxWidth));

#if UNITY_5 || UNITY_2017_1
				GUIStyle yellowTextStyle = new GUIStyle();
				yellowTextStyle.normal.textColor = Color.yellow;
				EditorGUILayout.LabelField("Use Build Settings to switch the Build Target to Standalone Windows x64.", yellowTextStyle);
#else // #elif UNITY_2017_2_OR_NEWER
				allDone = false;

				if (GUILayout.Button("Switch"))
				{
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
				}
#endif
				EditorGUILayout.EndHorizontal();
			}

			bool useDefaultGraphicsAPI = PlayerSettings.GetUseDefaultGraphicsAPIs(curBuildTarget);
			if (curBuildTarget == BuildTarget.StandaloneWindows || curBuildTarget == BuildTarget.StandaloneWindows64)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Do not use default graphics API", GUILayout.MaxWidth(_leftLabelMaxWidth));
				if (useDefaultGraphicsAPI == true)
				{
					allDone = false;

					if (GUILayout.Button("Disable"))
					{
						PlayerSettings.SetUseDefaultGraphicsAPIs(curBuildTarget, false);
					}
				}
				else
				{
					EditorGUILayout.LabelField("(Is disabled)");
				}
				EditorGUILayout.EndHorizontal();

				if (useDefaultGraphicsAPI == false)
				{
					List<UnityEngine.Rendering.GraphicsDeviceType> graphicsDeviceTypes = new List<UnityEngine.Rendering.GraphicsDeviceType>(PlayerSettings.GetGraphicsAPIs(curBuildTarget));

					if (graphicsDeviceTypes.Contains(_graphicsDeviceGLCore) == false) // If glcore is not available at all:
					{
						allDone = false;

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("Missing GLCore Graphics API", GUILayout.MaxWidth(_leftLabelMaxWidth));

						if (GUILayout.Button("Add"))
						{
							graphicsDeviceTypes.Insert(0, _graphicsDeviceGLCore);
							PlayerSettings.SetGraphicsAPIs(curBuildTarget, graphicsDeviceTypes.ToArray());
						}
						EditorGUILayout.EndHorizontal();
					}
					if (graphicsDeviceTypes.Contains(_graphicsDeviceDx11) == false) // If dx11 is not available at all:
					{
						allDone = false;

						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField("Missing DircetX 11 Graphics API", GUILayout.MaxWidth(_leftLabelMaxWidth));

						if (GUILayout.Button("Add"))
						{
							graphicsDeviceTypes.Add(_graphicsDeviceDx11);
							PlayerSettings.SetGraphicsAPIs(curBuildTarget, graphicsDeviceTypes.ToArray());
						}
						EditorGUILayout.EndHorizontal();
					}

					_graphicsApiFoldout = EditorGUILayout.Foldout(_graphicsApiFoldout, "Graphic APIs");
					if (_graphicsApiFoldout == true)
					{
						if (graphicsDeviceTypes.Count == 0)
						{
							EditorGUI.indentLevel++; // Start Intend Level Graphic APIs
							EditorGUILayout.LabelField("(empty)");
							EditorGUI.indentLevel--; // End Intend Level Graphic APIs
						}
						else
						{
							for (int ii = 0; ii < graphicsDeviceTypes.Count; ++ii)
							{
								UnityEngine.Rendering.GraphicsDeviceType graphicDeviceType = graphicsDeviceTypes[ii];

								EditorGUILayout.BeginHorizontal();

								EditorGUI.indentLevel++; // Start Intend Level Graphic APIs
								EditorGUILayout.LabelField(graphicDeviceType.ToString(), GUILayout.MaxWidth(_leftLabelMaxWidth));
								EditorGUI.indentLevel--; // End Intend Level Graphic APIs

								if (graphicDeviceType == _graphicsDeviceGLCore || graphicDeviceType == _graphicsDeviceDx11) // If it is the OpenGL or DX11 graphics API:
								{
									EditorGUILayout.LabelField("(Is available)");
								}
								else // Another API, but not OpenGL nor DX11
								{
									if (GUILayout.Button("Remove"))
									{
										graphicsDeviceTypes.RemoveAt(ii);
										PlayerSettings.SetGraphicsAPIs(curBuildTarget, graphicsDeviceTypes.ToArray());
									}
								}
								EditorGUILayout.EndHorizontal();
							}
						}
					}
				}
			}
			else
			{
				EditorGUILayout.LabelField("Switch to Standalone Windows plattform to setup Graphics API.");
			}

			return allDone;
		}

		private void SetupGraphicAPIs()
		{
			BuildTarget curBuildTarget = EditorUserBuildSettings.activeBuildTarget;

			// Set Build Target to Windows 64, if this is not already the case:
			if (curBuildTarget != BuildTarget.StandaloneWindows64)
			{
#if UNITY_5 || UNITY_2017_1
				GUIStyle yellowTextStyle = new GUIStyle();
				yellowTextStyle.normal.textColor = Color.yellow;
				EditorGUILayout.LabelField("Use Build Settings to switch the Build Target to Standalone Windows x64.", yellowTextStyle);
#else // #elif UNITY_2017_2_OR_NEWER
				if (GUILayout.Button("Switch"))
				{
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
				}
#endif
			}

			// Disable to use default graphics APIs:
			if (PlayerSettings.GetUseDefaultGraphicsAPIs(curBuildTarget) == true)
			{
				PlayerSettings.SetUseDefaultGraphicsAPIs(curBuildTarget, false);
			}

			// Set GraphicAPIs GlCore and DirectX 11:
			UnityEngine.Rendering.GraphicsDeviceType[] graphicsDeviceTypes = new UnityEngine.Rendering.GraphicsDeviceType[] { _graphicsDeviceGLCore, _graphicsDeviceDx11 };
			PlayerSettings.SetGraphicsAPIs(curBuildTarget, graphicsDeviceTypes);
		}

		// Returns true, if everything is done:
		private bool ManageDisplayResolutionDialog()
		{
			bool allDone = true;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Display Resolution Dialog", "Hide the Resolution Dialog by default. It can still be displayed by holding Shift at the start of the application."), EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));

			ResolutionDialogSetting dialogSetting = PlayerSettings.displayResolutionDialog;

			if (dialogSetting != ResolutionDialogSetting.HiddenByDefault)
			{
				allDone = false;

				if (GUILayout.Button("Hide by Default"))
				{
					HideDisplayResolutionDialog();
				}
			}
			else
			{
				EditorGUILayout.LabelField("(Is hidden)");
			}

			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		private void HideDisplayResolutionDialog()
		{
			PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;
		}

		// Returns true, if everything is done:
		private bool ManageSplashScreen()
		{
			bool allDone = true;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Unity Splash Screen", "Disable the Unity splash screen. In the DeepSpace a descriptive loading screen is displayed."), EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));


#if UNITY_5 || UNITY_2017_1
			bool showSplashScreen = PlayerSettings.showUnitySplashScreen;
#else // #elif UNITY_2017_2_OR_NEWER
			bool showSplashScreen = PlayerSettings.SplashScreen.show;
#endif

			if (showSplashScreen == true)
			{
				allDone = false;

				if (GUILayout.Button("Disable"))
				{
					DisableSplashScreen();
				}
			}
			else
			{
				EditorGUILayout.LabelField("(Is disabled)");
			}

			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		private void DisableSplashScreen()
		{
#if UNITY_5 || UNITY_2017_1
			bool showSplashScreen = PlayerSettings.showUnitySplashScreen;
#else // #elif UNITY_2017_2_OR_NEWER
			bool showSplashScreen = PlayerSettings.SplashScreen.show;
#endif

			if (showSplashScreen == true)
			{
#if UNITY_5 || UNITY_2017_1
				PlayerSettings.showUnitySplashScreen = false;
#else // #elif UNITY_2017_2_OR_NEWER
				PlayerSettings.SplashScreen.show = false;
#endif
			}
		}

		// Returns true, if everything is done:
		private bool ManageInputSettings()
		{
			bool allDone = true;

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(new GUIContent("Set up InputManager axes", "Input axes are required to use the xBox controller in the Deep Space. Buttons will work without this, axes set up joysticks and triggers."), EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));

			if (InputManagerEditor.AxisDefined(_axisHorizontalLeft) &&
				InputManagerEditor.AxisDefined(_axisVerticalLeft) &&
				InputManagerEditor.AxisDefined(_axisHorizontalRight) &&
				InputManagerEditor.AxisDefined(_axisVerticalRight) &&
				InputManagerEditor.AxisDefined(_axisDPadHorizontal) &&
				InputManagerEditor.AxisDefined(_axisDPadVertical) &&
				InputManagerEditor.AxisDefined(_axisShoulderTriggerL2) &&
				InputManagerEditor.AxisDefined(_axisShoulderTriggerR2))
			{
				EditorGUILayout.LabelField("(All set up)", EditorStyles.label);
			}
			else
			{
				allDone = false;

				if (GUILayout.Button("Set it up"))
				{
					AddRequiredInputAxes();
				}
			}

			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		private void AddRequiredInputAxes()
		{
			if (InputManagerEditor.AxisDefined(_axisHorizontalLeft) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisHorizontalLeft, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 1 });
			}
			if (InputManagerEditor.AxisDefined(_axisVerticalLeft) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisVerticalLeft, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 2, invert = true });
			}
			if (InputManagerEditor.AxisDefined(_axisHorizontalRight) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisHorizontalRight, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 4 });
			}
			if (InputManagerEditor.AxisDefined(_axisVerticalRight) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisVerticalRight, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 5, invert = true });
			}
			if (InputManagerEditor.AxisDefined(_axisDPadHorizontal) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisDPadHorizontal, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 6 });
			}
			if (InputManagerEditor.AxisDefined(_axisDPadVertical) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisDPadVertical, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 7 });
			}
			if (InputManagerEditor.AxisDefined(_axisShoulderTriggerL2) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisShoulderTriggerL2, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 9 });
			}
			if (InputManagerEditor.AxisDefined(_axisShoulderTriggerR2) == false)
			{
				InputManagerEditor.AddAxis(new InputManagerEditor.InputAxis() { name = _axisShoulderTriggerR2, dead = 0.19f, sensitivity = 1f, type = InputManagerEditor.AxisType.JoystickAxis, axis = 10 });
			}
		}

		// Returns true, if everything is done:
		private bool ManageAudioSettings()
		{
			bool allDone = true;

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Speaker Mode", EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));

			AudioConfiguration audioConfig = AudioSettings.GetConfiguration();

			if (audioConfig.speakerMode == AudioSpeakerMode.Mode5point1)
			{
				EditorGUILayout.LabelField("(Surround 5.1)", EditorStyles.label);
			}
			else
			{
				allDone = false;

				if (GUILayout.Button("Set to Surround 5.1"))
				{
					SetupSurroundSound();
				}
			}

			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		private void SetupSurroundSound()
		{
			AudioConfiguration audioConfig = AudioSettings.GetConfiguration();
			audioConfig.speakerMode = AudioSpeakerMode.Mode5point1;
			AudioSettings.Reset(audioConfig);
		}

		private bool ManageQualitySettings()
		{
			bool allDone = true;

			GUIStyle wordWrapTextStyle = new GUIStyle();
			wordWrapTextStyle.normal.textColor = Color.gray;
			wordWrapTextStyle.wordWrap = true;
			wordWrapTextStyle.padding = new RectOffset(5, wordWrapTextStyle.padding.right, wordWrapTextStyle.padding.top, 0);
			EditorGUILayout.LabelField("Due to a known bug, non head-mounted stereo does not work if anti aliasing is enabled.", wordWrapTextStyle);

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(new GUIContent("Anti Aliasing", "Disable Anti Aliasing to avoid issues with rendering in non head-mounted stereo. We only want to change the default Quality, so it is necessary to select it before it can be changed."),
				EditorStyles.label, GUILayout.MaxWidth(_leftLabelMaxWidth));

			string curQualityName = QualitySettings.names[QualitySettings.GetQualityLevel()];

			bool defaultIsSelected = true;

			// Check, if the default Quality Level is currently selected:
			Dictionary<string, int> platformQualityIndices = GetDefaultQualityForPlatforms();
			int defaultQualityIndex = QualitySettings.GetQualityLevel();
			if (platformQualityIndices.TryGetValue("Standalone", out defaultQualityIndex))
			{
				if (defaultQualityIndex != QualitySettings.GetQualityLevel())
				{
					defaultIsSelected = false;
					allDone = false;

					// If not, this button selects the default quality setting:
					if (GUILayout.Button("Select default \"" + QualitySettings.names[defaultQualityIndex] + "\""))
					{
						QualitySettings.SetQualityLevel(defaultQualityIndex, true);
					}
				}
			}

			// If the default is selected, AA can be disabled:
			if (defaultIsSelected == true)
			{
				if (QualitySettings.antiAliasing == 0)
				{
					EditorGUILayout.LabelField("(Disabled for " + curQualityName + ")", EditorStyles.label);
				}
				else
				{
					allDone = false;

					if (GUILayout.Button("Disable AA for \"" + curQualityName + "\""))
					{
						DisableAntiAliasing();
					}
				}
			}

			EditorGUILayout.EndHorizontal();

			return allDone;
		}

		// Parsing the QualitySettings.asset for the default quality indices per platform:
		private Dictionary<string, int> GetDefaultQualityForPlatforms()
		{
			SerializedObject qualitySettingsObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/QualitySettings.asset")[0]);
			SerializedProperty perPlatformDefaultQualityProperty = qualitySettingsObject.FindProperty("m_PerPlatformDefaultQuality");

			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (SerializedProperty serializedProperty in perPlatformDefaultQualityProperty)
				dictionary.Add(serializedProperty.FindPropertyRelative("first").stringValue, serializedProperty.FindPropertyRelative("second").intValue);
			return dictionary;
		}

		// Disables Anti Aliasing for the Standalon default Quality Setting.
		private void DisableAntiAliasing()
		{
			// Figure out the default quality index for Standalone:
			Dictionary<string, int> platformQualityIndices = GetDefaultQualityForPlatforms();
			int defaultQualityIndex = QualitySettings.GetQualityLevel();
			if (platformQualityIndices.TryGetValue("Standalone", out defaultQualityIndex))
			{
				// Select default quality if it is currently not selected:
				if (defaultQualityIndex != QualitySettings.GetQualityLevel())
				{
					QualitySettings.SetQualityLevel(defaultQualityIndex, true);
				}
			}

			// Disable Anti Aliasing for the default quality setting:
			QualitySettings.antiAliasing = 0;
		}

		private void ApplyAllRecommendedSettings()
		{
			EnableVirutalRealitySupport();
			SetupVirtualRealitySDKs();
			SetupGraphicAPIs();
			HideDisplayResolutionDialog();
			DisableSplashScreen();
			AddRequiredInputAxes();
			SetupSurroundSound();
			DisableAntiAliasing();
		}
	}
}