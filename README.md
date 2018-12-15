# HumanNumberToInt
convert english and farsi digit to word and vice versa
برای تبدیل اعداد به متن و متن به عدد در انگلیسی و فارسی


            var s1 = HumanNumber.ParseEnglish("three hundred and four thousand");

            var s2 = HumanNumber.ParseFarsi("هزار و سیصد و چهل");
            
            var i = HumanNumber.IntegerToWritten(-2005);

            var i2 = HumanNumber.IntegerToWrittenFA(-2005);
