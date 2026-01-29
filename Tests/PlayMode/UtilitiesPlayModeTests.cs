using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utilities.Runtime;

namespace Utilities.Tests.PlayMode.Tests
{
    public sealed class UtilitiesPlayModeTests
    {
        private Canvas _canvas;
        
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _canvas = new GameObject("Canvas", typeof(Canvas)).GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SetCanvasGroupAlpha_FadesCorrectly()
        {
            GameObject obj = new GameObject("Group", typeof(CanvasGroup));
            CanvasGroup group = obj.GetComponent<CanvasGroup>();
            group.alpha = 0f;

            yield return RuntimeUtilities.SetCanvasGroupAlpha(group, 1f, 0.25f);

            Assert.That(group.alpha, Is.EqualTo(1f).Within(0.05f));
        }

        [UnityTest]
        public IEnumerator TimeCalculator_MeasuresTimeAccurately()
        {
            TimeCalculator.StartTimer();
            
            yield return new WaitForSecondsRealtime(0.3f);
            
            float elapsed = TimeCalculator.StopTimer("Test");

            Assert.That(elapsed, Is.GreaterThan(0.2f).And.LessThan(1f));
        }
        
        [UnityTest]
        public IEnumerator TransformExtensions_SetXPosition_WorksCorrectly()
        {
            GameObject obj = new GameObject("TestObject");
            Transform tr = obj.transform;
            tr.position = new Vector3(1, 2, 3);

            tr.SetAxes(10f);

            Assert.AreEqual(10f, tr.position.x);
            Assert.AreEqual(2f, tr.position.y);
            Assert.AreEqual(3f, tr.position.z);

            Object.Destroy(obj);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TransformExtensions_LookAtWithAxis_AppliesRotation()
        {
            GameObject obj = new GameObject("Looker");
            GameObject target = new GameObject("Target")
            {
                transform =
                {
                    position = new Vector3(0, 0, 10)
                }
            };

            obj.transform.LookAtWithAxis(target.transform, Vector3.up, 45f);

            Assert.That(obj.transform.forward.z, Is.GreaterThan(0.5f));

            Object.Destroy(obj);
            Object.Destroy(target);
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator LookAtGradually_RotatesTowardsTarget()
        {
            GameObject sourceObject = new GameObject("Source");
            Transform sourceTransform = sourceObject.transform;
            GameObject targetObject = new GameObject("Target");
            Transform targetTransform = targetObject.transform;
            targetTransform.position = new Vector3(1f, 0f, 0f);

            Vector3 axis = Vector3.up;
            float maxRadiansDelta = Mathf.PI / 4f;

            sourceTransform.rotation = Quaternion.identity;

            float startTime = Time.time;
            while (Quaternion.Angle(sourceTransform.rotation, Quaternion.LookRotation(targetTransform.position - sourceTransform.position)) > 1f)
            {
                sourceTransform.LookAtGradually(targetTransform, axis, maxRadiansDelta);
                yield return null;
                if (!(Time.time - startTime > 5f))
                    continue;
                
                Assert.Fail("Rotation towards target took too long.");
            }

            Vector3 targetDirection = (targetTransform.position - sourceTransform.position).normalized;
            Vector3 currentForward = sourceTransform.forward;

            Assert.IsTrue(Vector3.Dot(targetDirection, currentForward) > 0.99f,
                $"Expected forward direction: {targetDirection}, Actual forward direction: {currentForward}");

            Object.Destroy(sourceObject);
            Object.Destroy(targetObject);
        }
        
        [UnityTest]
        public IEnumerator TransformExtensions_FindRecursive_FindsChildByName()
        {
            GameObject parent = new GameObject("Parent");
            GameObject child = new GameObject("Child");
            
            child.transform.SetParent(parent.transform);

            Transform found = parent.transform.FindRecursive("Child");

            Assert.NotNull(found);
            Assert.AreEqual("Child", found.name);

            Object.Destroy(parent);
            
            yield return null;
        }
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_canvas.gameObject);
            
            yield return null;
        }
    }
}