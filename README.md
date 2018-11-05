# BubbleShooter
A test project for all limbo tools

This project include a hotfix solution which based on 'ILRuntime Framework'.
Use assembly definition to splite the project to several part.
We define a assembly named 'HotFixed', this is the most importent assembly.
The unity refrences properity for this assembly is marked as test, 
it will only be used for tests and will not be included in player builds. 
We can use this feature to splite our hotfix scripts from main project and avoid to build it by ourself.