---
title: Storyboard
layout: default
---

This document outlines the user-interface design decisions we have made as part of our project.

The purpose of this document is to detail the experience the user will have while using our product, and to describe the possible interactions a user might have with the application.

## Table of Contents

* This will become a table of contents (this text will be scraped).
{:toc}

## Glossary

- FOV - Field of view, the space that is visible from the user's perspective.
- Instruction Set - A series of instructional slides that the user will interact with to demonstrate proper assembly of an object.
- UI - User interface, the part of the application that is directly visible to the user.
- Workspace - The flat surface where the user will interact with the instructions.

## Interface Elements

The Hololens software allows you to place an application in space or on a surface so that you can interact with it. Applications that remain in the user's FOV can be distracting and are disruptive to whatever the user is working on at the time.

The UI will utilize three separate elements in total: a welcome/splash screen, an main window, and a model.

### Welcome/Splash Screen

The welcome/splash screen will be visible to the user upon opening the application and will contain simple information, such as a logo, a trademark symbol, and a small, single-line instruction to place the window on a flat surface. Something like: "To begin, place the application on a flat surface near your workspace." It is important that the welcome/splash screen instruction should specify that the window should be placed near the user's workspace. It may be beneficial to include a small graphic for clarity and to demonstrate the importance of this step.

### Main Window

Once the application has been placed on a flat surface, the welcome/splash screen will be replaced by the main window, which will contain a menu and also serve to display instructions for the user later on.

When the main window is displayed, it will begin by opening up a menu for the user to interact with, containing options for interacting with the program, such as: Start, Settings, Instructions, and Resume, as appropriate and as features expand.

- The Start button will begin an Instruction Set, prompting the user to place the model in the workspace.

- The Resume button will continue a previously started Instruction Set.

- The Instructions button will display a list of all possible Instruction Sets that the user can work with.

- The Settings button will display a window that allows the user to select options which alter the user experience or change the functionality of the application.

#### Start Button

When the user presses the Start button, they will be given a prompt, either a text prompt or audio prompt, to place the model on the workspace. Once the user has placed the model on the workspace, the main window will change into the instruction window, detailed later in this document ([[Instruction Window]]). The application will then begin the Instruction Set at the first step.

The Start button will be disabled until the user selects an Instruction Set to work with using the Instructions button.

#### Resume Button

__STRETCH__
When the user presses the Resume button, they will be given the same prompt as the Start button, but the application will attempt to begin the Instruction Set at the last completed instruction, unless the last completed instruction was the last step, in which case, the application will ask the user whether or not they want to start over.

The Resume button will be disabled unless the user has previously selected an Instruction Set to work with using the Instructions button.

#### Instructions Button

When the user presses the Instructions button, they will be shown a list of all available Instruction Sets in a window that replaces the main window. The Instruction Sets will be displayed either in a list format or in some other format, such as icons, that allow the user to navigate through the available instruction sets and choose which Instruction Set they wish to work with. Once they have chosen which instruction set they want to work with, the users will be taken back to the main window.

#### Settings Button

__STRETCH__
When the user presses the Settings button, they will be shown a list of all available settings in a window that replaces the main window. Similar to the Instructions window, the window will display all available settings in list format, or as icons. There will also be a button in this window which the user will be able to select, such as an OK or Cancel button that will take them back to the main window.

### Instructions Window

The instructions window is distinct from the window that is part of the main menu which asks the user to choose a model to work with.

The instruction window is composed of four main parts for navigating the application. It will include text, describing the current step in the Instruction Set, which will be centered in the window. It will include navigation buttons, such as a Next, Previous, and Exit button which will allow the user to navigate the Instruction Set.

#### Text Window

The user will not be able to interact with the text window directly. The window will display text for the current step in the Instruction Set.

#### Next Button

When the user presses the Next button, the Instruction Set will advance one step, changing the text in the instruction window and updating the model.

__STRETCH__
It will also be possible for the user to navigate to the next step by using voice controls, such as "Next," "Go forward," etc.

#### Previous Button

When the user presses the Previous button, the Instruction Set will reverse one step, changing the text in the instruction window and updating the model.

__STRETCH__
It will also be possible for the user to navigate to the next step by using voice controls, such as "Previous," "Back," "Go back," etc.

#### Exit Button

When the user presses the Exit button, the application will return to the main window and save the current step in the Instruction Set so that the user can easily return to the instruction window at the current step. If the user selects the Exit button, the model will also disappear from the workspace.

### The Model

After the user chooses the Start or Resume buttons from the main window, the user will be asked to place an anchor point somewhere in the workspace for the model. The prompt will ask them to place the model somewhere near the workspace where it is clearly visible to the user, something like, "Place the model near your workspace."

It will be possible for the user to have limited interaction with the model. It will be possible to move the anchor point for the model, for example.

The model will display a unique model demonstrating each step in the Instruction Set. The model will change to a ew model as the user navigates through the Instruction Set.

__STRETCH__
The model will also animate each step, using a model of the "parent" object and a "child" model to demonstrate how the "child" fits into the "parent."

__STRETCH__
It will be possible for the user to interact with the "child" model by selecting the child model and dragging it around the workspace.

#### Model Animation

__STRETCH__
The model will animate each step using the "child" model. The "child" model will display an animation for how it fits into the "parent". The user will be able to select the "child" model and move it around within the workspace. If the user moves the "child" model into the correct place relative to the parent, the entire model will display a visual notification that the "child" model was placed correctly. For example, the entire model will be outlined in green.

## Stretch Features

Stretch features may be implemented, may be partially implemented, or may not be implemented at all, depending on time constraints.

### Parts List

__STRETCH__
When the user begins an Instruction Set, they will be shown a list of all parts that are required for the Instruction Set and be asked to compare the real-world objects in their workspace with the models to ensure they have all parts necessary to complete the Instruction Set.

The parts models will display life-size models of the parts, so that the user can compare what they have in their workspace with what they need as a one-to-one comparison.

### Current Parts List

__STRETCH__
On the instruction window, there will be models which display the objects which are required for the current step. These will be visible to the user either underneath the text display or as a separate option for the user.

### Object Recognition

__STRETCH__
The user will be able to analyze the parts they have in their workspace using the Hololens's cameras. The user will be able to scan a part to ensure that it is the correct part for a certain step or analyze the entire assembly to ensure that they have completed a step correctly.

This feature will be accessible as either a Detect button, placed somewhere near the model, or as a setting that is enabled in the settings window and then required after each step.
