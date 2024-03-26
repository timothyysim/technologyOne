using Microsoft.AspNetCore.Mvc;
using NumToWords.Models;
using System.Diagnostics;
using System.Numerics;

namespace NumToWords.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public List<string> Ones = ["", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        public List<string> Tens = ["", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];
        public List<string> Teens = ["ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"];
        public List<string> Hundreds = ["hundred"];
        public List<string> Thousandths = ["", "thousand", "million", "billion", "trillion"];


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ConvertUsingBatchSet(BigInteger number)
        {
            string NumWord = "";
            if (number == 0)
            {
                NumWord = "Zero";
            }
            if (number != 0)
            {
                //Count how many in thousands list to prevent crash
                int totalThousandCount = Thousandths.Count();
                for (int i = 0; number > 0; i++)
                {
                    //Split digits by 3 by finding the modulus using 1000
                    if (number % 1000 != 0)
                    {
                        int set = (int)(number % 1000);
                        string setWord = "";

                        //if has 3 digits use hundreds
                        if (set >= 100)
                        {
                            setWord += Ones[set / 100] + " Hundred ";
                            set %= 100;
                        }

                        if (set >= 10 && set <= 19)
                        {
                            setWord += Teens[set % 10] + " ";
                        }
                        else
                        {
                            setWord += Tens[set / 10] + " ";
                            setWord += Ones[set % 10] + " ";
                        }
                        if (i >= totalThousandCount)
                        {
                            NumWord = "Cannot convert number as it is too big. The highest number that can be converted would be up to " + Thousandths[totalThousandCount - 1];
                            ViewBag.Result = NumWord;
                            return View("Index");
                        }
                        else
                            NumWord = setWord + Thousandths[i] + " " + NumWord;
                    }
                    number /= 1000;
                }
                NumWord = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(NumWord.Trim().ToLower());
                ViewBag.Result = NumWord;
                return View("Index");
            }
            else
            {
                NumWord = "Invalid input. Please enter a valid number";
                ViewBag.Result = NumWord;
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult ConvertNumToText(int number)
        {
            string NumWord = "";
            //Check how many digits are there
            int totalDigit = number.ToString().Count();
            if (totalDigit > 3)
            {
                NumWord = "Unable to compute. Kindly use Humanizer.";
            }
            else if (number == 0)
            {
                NumWord = "Zero";
            }
            else if (totalDigit == 1) // ones
            {
                NumWord = Ones[number];
            }
            else if (totalDigit == 2 && number < 20) // teens
            {
                NumWord = Teens[number - 10];
            }
            else if (totalDigit == 2 && number >= 20) // tens 
            {
                var FirstDigit = number / 10;
                var SecondDigit = number % 10;
                NumWord = Tens[FirstDigit] + " " + Ones[SecondDigit];
            }
            else if (totalDigit == 3) // hundreds
            {
                var FirstDigit = number / 100;
                var SecondDigit = (number % 100) / 10;
                var ThirdDigit = (number % 100) % 10;
                if (SecondDigit == 0 && ThirdDigit == 0)
                {
                    NumWord = Ones[FirstDigit] + " Hundred";
                }
                else if ((number % 100) < 20)
                {
                    NumWord = Ones[FirstDigit] + " Hundred and " + Teens[ThirdDigit];
                }
                else
                {
                    NumWord = Ones[FirstDigit] + " Hundred and " + Tens[SecondDigit] + " " + Ones[ThirdDigit];
                }
            }
            NumWord = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(NumWord.Trim().ToLower());
            ViewBag.Result = NumWord;
            return View("Index");
        }



    }
}
