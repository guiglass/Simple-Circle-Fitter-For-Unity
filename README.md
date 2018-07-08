# Simple Circle Fitter
### Single script, fully self contained algorithms.

Two scripts that show two differnt approaches to fitting a circle to 2d data points on a plane. 
Both scripts are written in c# and are entirely self stand alone, requiring that you only need drag the desired script and add as a component to whatever gameobject you wish. THne add the transforms to be fitted to in the coordinates list.

Note that with any circle you must specify at least three coordinates in order to define it, axiomatically, an infinite number of circles can be defined by trying to use two or less coordinates. Thus it is a requirement for these scripts to function that you always have 3 or more coordinates in available in the arrays!

**Video:**

_An example and tutorial video on how to use the scripts:_

[![Youtube Video](https://img.youtube.com/vi/90KesqCDDog/0.jpg)](https://www.youtube.com/watch?v=90KesqCDDog)



It would be a kind gesture if you did leave some attribution somewhere near any functions you copy, if you do copy any, such as:

> //Credits: Grant Olsen - 2018

Or you may leave the credit in the original file, however none of these are a requirement (I consider this as CC-Zero, so please enjoy :D ).



Feel free to use this example in any way you see fit (without restriction of any kind) as well as redistribute, modify and share it with all of your friends and co-workers.

Legal notice:
By downloading or using any resource from this example you agree that I (the author) am not liable for any losses or damages due to the use of any part(s) of the content in this example. It is distributed as is and without any warranty or guarantees. 

*Project by: Grant Olsen (jython.scripts@gmail.com)
Creation year: 2018*

_Big credits to:_

* https://www.mathsisfun.com/algebra/matrix-multiplying.html for providing clear instructions on dot product!
* https://www.youtube.com/watch?v=YvjkPF6C_LI patrickJMT for an awesome explination on finding the inverse of a 3x3.
* https://mec560sbu.github.io/2016/08/29/Least_SQ_Fitting/ for the super efficient circle fitting concept.





