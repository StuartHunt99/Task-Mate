# Task-Mate

TaskMate is designed to help plan your day as efficiently and productively as possible. It works by sorting your task list according to the productivity principles of the Eisenhower Quadrant and then creating a time plan in 25 minute blocks (with 5 minute rests) according to the  Pomodoro Technique.

TaskMate customizes your day, so you can be sure your time is truly well-spent!


## Code Louisville Requirements
- **Feature 1:** 
Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program
 <br />
This is accomplished through the closed loop menu system.

- **Feature 2:**
Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program <br />
The master list of tasks are managed by a List<Task> object, which is initilized when the project is executed.  The user can add, edit, or delete items from this list.

- **Feature 3:** 
Read data from an external file, such as text, JSON, CSV, etc and use that data in your application <br />
The master list of tasks is stored and retrieved in a JSON file, so that the task list persists if the applicaiton is closed.  The loading and saving occurs via the Repo class.

- **Feature 4:** 
Use a LINQ query to retrieve information from a data structure (such as a list or array) or file <br/>
The master list is sorted according to certain properties via a LINQ query.  For example TaskListUtils.CreateBlockSchedule() sorts tasks first by urgency and then by importance.

- **Feature 5:** 
Build a conversion tool that converts user input to another type and displays it (ex: converts cups to grams) <br/>
I converted the string values of task.Minutes to int blocks of 25 minutes (5 minutes of rest,  which were then converted to actual DateTime values.   
  
- **Feature 6:** 
Read data from an external file, such as text, JSON, CSV, etc and use that data in your application <br />
Essential values are stored in APP.CONFIG, including block minutes, rest minutes, day plan start time, day plan end time, lunch time, and coffee break time.
  
  
- **Feature Changed From Initial Proposal:** 
Rather than assigning a type to each task and assigning a time value to certian tasks, I opted to calculate a more intelligent day plan according to the Eisenhower Quadrant and the Pomodoro Technique.  I found this feature to be more practical and useful for the user. <br/>


## Getting Started
1. Clone project 
2. Run via 'dotnet run' in source folder, or open project and build/run in Visual Studo
3. On first run, TaskMate will generate a demo task list.  Feel free to add, edit or delete tasks as needed.
4. Use ARROW keys + ENTER as primary means of navigation.

## Features:

- [ ] Create a master task list by adding, editing, and deleting tasks.
- [ ] View master list of tasks and task properties.
- [ ] Create a daily schedule that is arranged according to the Eisenhower Quadrant princlples and Pomodoro Technique.
- [ ] Automatically schedules lunch and coffee breaks.
- [ ] Automaticall add breaks to scheudle by including the word "break" in the task name.

##Todo
- [ ] Refine quadrant display for higher fidelity/placement accuracy

