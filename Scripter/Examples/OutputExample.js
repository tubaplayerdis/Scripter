//Variables
var TimesToLoop = 10
var FirstAdd = 5
var SecondAdd = 6
//Variables

//Basic Hello
Console.Log("Hello From Javascript");

// Run this loop as many times as defined by the variable above
for (var i = 0; i < TimesToLoop; i++) {
    Console.Log(`Saying HI :) for the ${String(i)} time!`)
}

//Call function and variables in Console.Log() call
Console.Log(`${String(FirstAdd)} plus ${String(SecondAdd)} Equals: ${AddNumber(FirstAdd, SecondAdd)}`)

//Function that returns string intepretation of x and z, You have to use String function when addresing returns in function 
//This is because Console.Log() only accepts string and putting a int type var through it will cause an error
function AddNumber(x, z) {
    return String(x + z);
}

