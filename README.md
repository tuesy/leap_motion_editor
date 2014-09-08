# Controlling Unity Using Leap Motion
### Screenshots
![GUI](http://mankindforward.files.wordpress.com/2014/09/screen-shot-2014-09-04-at-11-59-16-am.png?w=1200)
### Why?
I'm still waiting for the [Oculus DK2](https://www.oculusvr.com/order/) so I'm doing this project to find a more intuitive way to translate, scale, and rotate objects in the Unity. If you've seen the Iron Man movies or [Elon Musk designing rockets with his hands](https://www.youtube.com/watch?v=xNqs_S-zEBY), you'll understand what I'm going for. Manipulating 3D objects using keyboard and mouse is quite limiting. If we can improve on this, it would allow us to create content for VR faster.
### What?
We'll be using the [Leap Motion](https://www.leapmotion.com/), which ships in 2 days from Amazon :-). The plan is to create a custom Unity editor window that will communicate with the Leap Motion sensor. We'll then connect the hand movements directly to the translation, scale, and rotation controls for the currently selected GameObject. Feedback is always welcome. If it's good, I may put it up on the Asset Store to share more broadly.
### How?
* Clone this repo
* Open it in Unity
* Plug in your Leap Motion and use the visualizer to make sure it's sending data
* Open the custom editor window from Unity (Window Menu > Leap Motion)
* Click on a GameObject in your scene
* Place your hand(s) above the Leap Motion
* Try it out by holding down the Command/Control key and moving your hand (only translation and scaling are available right now)
* (optional) import a scene like the "Robot Lab" tutorial to play around with different objects

### Contributors
* Jimmy Zhang - [@mankindforward](https://twitter.com/mankindforward)

### License
Copyright (c) 2014 Jimmy Zhang See the LICENSE file for license rights and limitations (MIT).
