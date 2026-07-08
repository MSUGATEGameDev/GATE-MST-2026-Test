using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildScripts
{
    // Helper to get all enabled scenes in Build Settings
    static string[] GetEnabledScenes()
    {
        // Get all enabled scenes from Build Settings
        var scenesInSettings = EditorBuildSettings.scenes;
        var enabledScenes = new System.Collections.Generic.List<string>();

        // Iterate through all scenes and add the enabled ones
        for (int i = 0; i < scenesInSettings.Length; i++)
        {
            if (scenesInSettings[i].enabled)
                enabledScenes.Add(scenesInSettings[i].path);
        }

        return enabledScenes.ToArray();
    }

    // General build method
    static void BuildForTarget(BuildTarget target, string outputPath)
    {
        string[] scenes = GetEnabledScenes();

        // Platform-specific settings
        if (target == BuildTarget.Android)
        {
            // Basic Android PlayerSettings (optional)
            PlayerSettings.applicationIdentifier = "com.company.mygame";
            PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.Android.bundleVersionCode = 1;
            EditorUserBuildSettings.buildAppBundle = true; // Use AAB for Play Store
        }

        // Build player options
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = target,
            options = BuildOptions.None
        };

        // Execute the build
        BuildReport report = BuildPipeline.BuildPlayer(options);
        CheckBuildResult(report, outputPath);
    }

    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        BuildForTarget(BuildTarget.StandaloneWindows64, "Builds/Windows/MyGame.exe");
    }

    [MenuItem("Build/macOS")]
    public static void BuildMacOS()
    {
        BuildForTarget(BuildTarget.StandaloneOSX, "Builds/MacOS/MyGame.app");
    }

    [MenuItem("Build/Android (AAB)")]
    public static void BuildAndroidAAB()
    {
        BuildForTarget(BuildTarget.Android, "Builds/Android/MyGame.aab");
    }

    [MenuItem("Build/webGL")]
    public static void BuildWebGL()
    {
        BuildForTarget(BuildTarget.webgl, "Builds/WebGL");
    }

    // Helper to validate and log the build result
    static void CheckBuildResult(BuildReport report, string outputPath)
    {
        // Log the build summary
        var summary = report.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            UnityEngine.Debug.Log("Build succeeded at: " + outputPath + " (" + summary.totalSize + " bytes)");
        }
        else
        {
            throw new System.Exception("Build failed: " + report.SummarizeErrors());
        }
    }
}
