# Utilities
Utilities is a utility package designed to streamline common tasks in Unity projects. It offers various helper functions for tasks like transforming objects, calculating time, animating canvas groups, sorting lists, and more. This package is aimed at making Unity development faster and easier by providing essential tools right out of the box.

## Features
Utilities offers the following capabilities:
* Transform Extensions: Convenient methods to manipulate the position, rotation, and scale of GameObjects.
* FPS Display: A simple FPS counter to monitor performance in the Unity editor.
* Canvas Group Animation: Smoothly animate the alpha of a CanvasGroup over time.
* List Utilities: Functions for shuffling and sorting lists.
* Time Calculation: Tools to measure elapsed time during development.
* Editor Tools: Create new tags in the Unity editor.

## Getting Started
Install via UPM with git URL

`https://github.com/Emre-Emiroglu/Utilities.git`

Clone the repository
```bash
git clone https://github.com/Emre-Emiroglu/Utilities.git
```
This project is developed using Unity version 6000.2.6f2.

## Usage
* Transform Extensions: You can use various extension methods to manipulate the position and rotation of a transform.
    ```csharp
    transform.SetAxes(x: 1f, y: 2f, z: 3f, local: false);
    transform.LookAtWithAxis(target: target, axis: Vector3.up, angleOffset: 45f);
    transform.LookAtGradually(target: target, axis: Vector3.right, maxRadiansDelta: .1f, stableUpVector: false)
    transform.FindRecursive(name: "Child", includeInactive: false)
    ```

* Fps Display: Enable the FPS display to monitor performance.
    ```csharp
    FpsDisplay fpsDisplay = new GameObject("FpsDisplay").AddComponent<FpsDisplay>();
    ```

* Canvas Group Alpha Animation: Animate the alpha of a CanvasGroup over time.
    ```csharp
    StartCoroutine(RuntimeUtilities.SetCanvasGroupAlpha(canvasGroup, 0f, 1f));
    ```

* List Utilities: Shuffle or bubble sort a list with built-in functions.
    ```csharp
    List<int> shuffledList = RuntimeUtilities.Shuffle(list);
    List<int> sortedList = RuntimeUtilities.BubbleSort(list);
    ```

* Time Calculation: Measure time elapsed in your project.
    ```csharp
    TimeCalculator.StartTimer();
    // Your code...
    float elapsedTime = TimeCalculator.StopTimer("Task 1");
    ```

* Editor Tools: Create a tag in the Unity editor.
    ```csharp
    EditorUtilities.CreateTag("NewTag");
    ```

## Acknowledgments
Special thanks to the Unity community for their invaluable resources and tools.

For more information, visit the GitHub repository.
