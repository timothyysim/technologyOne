# ConvertNumToWords

## Name
Numeric Converter

## Description
Converts numeric values into words without using libraries or NuGet packages in ASP.NET Core Web Application project. 
Two Approcahes

Apporach No.1 : Total Digits
    - Counts the total digits and converts from left to right depending on total digits.
    - Improvements : 
        - To allow conversion above 1,000
    - Drawbacks : 
        - Repetitive code

Approach No.2 : Split by Batch 3
    - Splits the whole number into batches of 3. Everytime the batch no increases, that means the value of number increases
    - We can see that from a thousand (1,000) to a million (1,000,000) it increases by 3 digits which shows that for every 3 digits added after a thousand we can increase the numeric value e.g (thousand, million, billion, trillion, etc) 
    - Imporvements :
        - More proper naming like adding "and" where needed
        - Can added type of number, allow selection of numeric type (currency, percentage, decimal, etc.)
        

## Installation
Installation Steps : 

1. Clone out or Download the project. 
2. Navigate to convertnumtowords/DeploymentFile
3. Seach and double click file "NumToWords.exe"
4. If not directed to browser within 10 seconds, go to your browser and enter "localhost:5000"
5. Webpage will be displayed.

## Usage
Enter numeric values into textbox and click the convert button to view results,

## Roadmap
- New Updates coming!
    - Adding option to select numeric type (Currency, Decimal, Percentage)
    - Add option to increase the value limit (current is up to Trillion)

