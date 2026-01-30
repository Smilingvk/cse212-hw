using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue to verify highest priority is removed first
    // Expected Result: Items should be dequeued in order of priority (highest first): "High" (priority 3), "Medium" (priority 2), "Low" (priority 1)
    // Defect(s) Found: The loop in Dequeue only iterates to _queue.Count - 1, missing the last element. Item is not removed from queue after dequeue. Uses >= instead of > for priority comparison (picks last instead of first for same priority).
    public void TestPriorityQueue_BasicPriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 2);
        priorityQueue.Enqueue("High", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same priority to verify FIFO order for equal priorities
    // Expected Result: For items with same priority, the first one enqueued should be dequeued first: "First" then "Second" then "Third"
    // Defect(s) Found: Uses >= instead of > which picks the LAST item with highest priority, violating FIFO requirement.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Mix of priorities with some equal - verify both priority ordering and FIFO for equal priorities
    // Expected Result: "VIP1" (7), "VIP2" (7), "Normal1" (3), "Normal2" (3), "Low" (1)
    // Defect(s) Found: Multiple defects compound here - loop doesn't check last element, items aren't removed, and >= picks wrong item for same priority.
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Normal1", 3);
        priorityQueue.Enqueue("VIP1", 7);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Normal2", 3);
        priorityQueue.Enqueue("VIP2", 7);

        Assert.AreEqual("VIP1", priorityQueue.Dequeue());
        Assert.AreEqual("VIP2", priorityQueue.Dequeue());
        Assert.AreEqual("Normal1", priorityQueue.Dequeue());
        Assert.AreEqual("Normal2", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None for this specific requirement - exception handling works correctly.
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue single item and dequeue it - tests that last item in queue can be accessed
    // Expected Result: Should successfully dequeue "OnlyItem"
    // Defect(s) Found: If queue has only one item at index 0, the loop never runs (starts at 1, condition is < 0), so index stays 0 and it works by accident. But with multiple items, last item is never checked.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("OnlyItem", 1);

        Assert.AreEqual("OnlyItem", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Verify that item with highest priority at the END of the queue is found
    // Expected Result: Should dequeue "HighAtEnd" even though it was added last
    // Defect(s) Found: Loop condition _queue.Count - 1 means last element is never checked. "HighAtEnd" would not be found.
    public void TestPriorityQueue_HighPriorityAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("Low2", 1);
        priorityQueue.Enqueue("HighAtEnd", 10);

        Assert.AreEqual("HighAtEnd", priorityQueue.Dequeue());
    }
}