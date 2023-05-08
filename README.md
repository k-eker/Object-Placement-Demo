# Object Placement Demo

## Description

This is a mobile Unity app that allows the user to navigate in a 3D environment and place objects. User's creations are saved on the device, and they will be loaded exactly as they left it.There's also an undo redo system which reverts the user's most recent actions.

## Approach

I first analyzed the requirements and planned the architecture. I chose to go with the singleton design pattern due to the scale of the application. I prioritized modularity and expandability by creating mostly independent systems. I knew that the user experience was important, so I spent a lot of time polishing the application.

## Technologies Used

| Tool             | Description                                                                   |
| ---------------- | ----------------------------------------------------------------------------- |
| DOTween          | Tweening library used to quickly animate objects.                             |
| Hierarchy Gadget | Editor utility package which adds more functionality to the hierarchy window. |
| LunarConsole     | Debugging tool that shows the console on mobile devices.                      |

## Installation and Setup

To open this project, you'll need to install Unity version 2021.3.19.

Alternatively, you can install the apk file on an Android device. The apk file is located at **Builds/Demo.apk**

## Challenges

Here is some of the challenges I faced while working on this project:

- The most notable challenge to overcome was scaling the selection UI based on the mesh boundaries of the selectable object. I managed to solve this problem by raycasting each corner of the mesh and getting the outer most corner positions. Then, I converted these world positions into screen positions while also factoring the canvas scaler. I found [Quill18's tutorial](http://quill18.com/unity_tutorials/) titled "3 ways to indicate selected units" very useful.

- I also struggled a little with the camera controls, but ended up with perfect results in the end. Implementing the mouse controls was easy. However, touch controls took some time to implement. I created a ground plane which allowed me to raycast from the camera to and calculate the world distance between multiple touches. I got inspired by [DitzelGames's tutorial](https://www.youtube.com/watch?v=KkYco_7-ULA&t=7s&ab_channel=DitzelGames) to achieve this.

- Data persistance was one of the requirements. I didn't want to use PlayerPrefs even though the scale of the application was small, so I went with JSON serialization to preserve data. This gave me an idea to store data of more than one project at the same time. This one wasn't as challenging as the others, but drawing thumbnail pictures on the projects was. Since serializing texture data is not possible, I had to convert the texture data into byte arrays to serialize it.

- Finally, having an undo redo system at first sounded like an easy task, as I had worked on similar tasks before. However, undoing an object that was recently created meant that this object would now be destroyed, losing all of its references in the undo actions. For example, undoing after adding an object always meant that the object no longer existed. In order to prevent this issue, I ended up disabling the objects rather than destroying them. This meant that I had to be careful with potential memory leaks, as objects could pile up in the scene. What I did to solve this was to implement a Dispose function to all undo/redo actions, which destroyed the referenced objects once the undo/redo action is removed from the stack.

## Improvements

| Issue                                                 | Suggestion                                                                                                                                                                                                |
| ----------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Architecture improvements                             | Assuming that a bigger team works on this project, the singleton pattern can be replaced with a more stable design pattern such as dependency injection, or factory pattern.                              |
| Placed objects should never intersect with each other | It would be nice to implement a system which checks if the colliders of multiple objects can intersect, improving the accuracy of objects not going inside of each other.                                 |
| More rotation options                                 | Currently, the user can only rotate the objects on the Y axis. More rotation options would be nice, but we should keep an eye out for potential gimbal locks.                                             |
| Selection UI can go outside of the screen boundaries  | If the user zooms into the object too much, the selection UI controls can go outside of the screen. We need to check this and set a limits for the selection UI.                                          |
| Deleting projects                                     | The current debug button in the project view titled "Delete All Projects" can be removed, and a new project deletion system can be implemented. This would allow the users to delete individual projects. |

## Conclusion

Despite all the challenges, I found working on this project fairly easy as I had worked on similar systems in the past. However, it was an excellent exercise to strengthen my skills in Unity. With some minor improvements, the application can become even better.
