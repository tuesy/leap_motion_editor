using UnityEditor;
using UnityEngine;
using System.Collections;
using Leap;
public class LeapMotionEditorWindow : EditorWindow
{
  bool active = false;
  float tSensitivity = 1500; // translation, scale, and rotation sensitivities
  float sSensitivity = 100000;
  float rSensitivity = 1;
  int handCount = 0;
  int fingerCount = 0;
  float frameRate;
  long frameID;
  long previousFrameID;
  float scale;
  float rotationDominance;
  float scaleDominance;
  float translationDominance;
  bool showProbabilities;
  bool showSensitivities = true;
  bool showMisc;
  Matrix rotationMatrix;
  Vector translation;
  Controller controller = new Controller ();
  // Add to the Window menu
  [MenuItem("Window/Leap Motion")]
  public static void ShowWindow()
  {
    //Show existing window instance. If one doesn't exist, make one.
    EditorWindow.GetWindow(typeof(LeapMotionEditorWindow));
  }
  void OnGUI()
  {

    showSensitivities = EditorGUILayout.Foldout(showSensitivities, "Sensitivities");
    if(showSensitivities){
      EditorGUILayout.LabelField ("Scale Sensitivity");
      sSensitivity = EditorGUILayout.Slider(sSensitivity,100000,500000);
      EditorGUILayout.LabelField ("Rotation Sensitivity");
      rSensitivity = EditorGUILayout.Slider(rSensitivity,1,100);
      EditorGUILayout.LabelField ("Translation Sensitivity");
      tSensitivity = EditorGUILayout.Slider(tSensitivity,1000,10000);
    }

    showProbabilities = EditorGUILayout.Foldout(showProbabilities, "Probabilities");
    if(showProbabilities){
      EditorGUILayout.LabelField ("Rotation Probability: ", rotationDominance.ToString());
      EditorGUILayout.LabelField ("Scale Probability: ", scaleDominance.ToString());
      EditorGUILayout.LabelField ("Translation Probability: ", translationDominance.ToString());
    }

    showMisc = EditorGUILayout.Foldout(showMisc, "Misc");
    if(showMisc){
      EditorGUILayout.LabelField ("Current Frame: ", frameID.ToString());
      EditorGUILayout.LabelField ("Previous Frame: ", previousFrameID.ToString());
      EditorGUILayout.LabelField ("Frame Rate: ", frameRate.ToString());
      EditorGUILayout.LabelField ("Hands: ", handCount.ToString());
      EditorGUILayout.LabelField ("Fingers: ", fingerCount.ToString());
      EditorGUILayout.LabelField ("Scale: ", scale.ToString());
    }

    active = EditorGUI.actionKey;
  }
  void Update()
  {
    if(controller.IsConnected) //controller is a Controller object
    {
      Frame frame = controller.Frame(); //The latest frame
      Frame previous = controller.Frame(1); //The previous frame

      if(!frame.IsValid || !previous.IsValid)
        Debug.Log("invalid frame");
      else
      {
        handCount = frame.Hands.Count;
        fingerCount = frame.Fingers.Count;

        frameID = frame.Id;
        previousFrameID = previous.Id;
        frameRate = frame.CurrentFramesPerSecond;

        scale = frame.ScaleFactor(previous) - (float)1.0;
        translation = frame.Translation(previous);
        rotationMatrix = frame.RotationMatrix(previous);
        Debug.Log(rotationMatrix);
        //Debug.Log(rotationMatrix.Rotation);
        //Debug.Log(rotationMatrix.UnityMatrixExtension.Rotation);

        rotationDominance = frame.RotationProbability(previous);
        scaleDominance = frame.ScaleProbability(previous);
        translationDominance = frame.TranslationProbability(previous);

        GameObject g = Selection.activeGameObject;
        if(g != null && active){ // check if a GameObject is selected
          // TODO: make the sensitivity adjustable in the GUI
          if(translationDominance > 0.75){
            g.transform.Translate(translation.ToUnity() * Time.deltaTime * tSensitivity);
          }else if(scaleDominance > 0.75)
          {
            float change = scale * sSensitivity * Time.deltaTime;
            g.transform.localScale += new Vector3(change, change, change);
          }else if(rotationDominance > 0.75){

          }
        }
      }
    }
    Repaint(); // update GUI
  }
}
