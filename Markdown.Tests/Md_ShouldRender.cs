using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Md_ShouldRender
    {
        public static IEnumerable<TestCaseData> SpecifictaionCases()
        {
            yield return new TestCaseData("Текст _окруженный с двух сторон_  одинарными символами подчерка должен помещаться в em")
                                 .Returns("Текст <em>окруженный с двух сторон</em>  одинарными символами подчерка должен помещаться в em")
                                 .SetName("Курсив");

            yield return new TestCaseData("\\_Вот это\\_ не выделяется em")
                                 .Returns("_Вот это_ не выделяется em")
                                 .SetName("Экранирование");

            yield return new TestCaseData("__Двумя символами__ — должен становиться жирным с помощью тега strong")
                                 .Returns("<strong>Двумя символами</strong> — должен становиться жирным с помощью тега strong")
                                 .SetName("Жирный");

            yield return new TestCaseData("Внутри __двойного выделения _одинарное_ тоже__ работает")
                                 .Returns("Внутри <strong>двойного выделения <em>одинарное</em> тоже</strong> работает")
                                 .SetName("Курсив внутри жирного");

            yield return new TestCaseData("Но не наоборот — внутри _одинарного __двойное__ не работает_.")
                                 .Returns("Но не наоборот — внутри <em>одинарного __двойное_</em> не работает_.")
                                 .SetName("Жирный внутри курсива - не работает");

            yield return new TestCaseData("Подчерки внутри текста c цифрами _12_3 __12__3 не считаются выделением")
                                 .Returns("Подчерки внутри текста c цифрами _12_3 __12__3 не считаются выделением")
                                 .SetName("Подчерки внутри цифр - не выделение");

            yield return new TestCaseData("Подчерки внутри слов не считаются выделением _h_e_l_l_o_world")
                                 .Returns("Подчерки внутри слов не считаются выделением _h_e_l_l_o_world")
                                 .SetName("Подчерки внутри слов - не выделение");

            yield return new TestCaseData("__непарные _символы не считаются выделением")
                                 .Returns("__непарные _символы не считаются выделением")
                                 .SetName("Непарные симовлы - не выделение");


            yield return new TestCaseData("Эти_ подчерки_ не считаются выделением ")
                                 .Returns("Эти_ подчерки_ не считаются выделением ")
                                 .SetName("Выделение не начинается, если за подчерком пробел");


            yield return new TestCaseData("_эти _подчерки _не считаются_ окончанием выделения")
                                 .Returns("<em>эти _подчерки _не считаются</em> окончанием выделения")
                                 .SetName("Выделение не заканчивается, если перед подчерком пробел");
        }

        [TestCaseSource(nameof(SpecifictaionCases))]
        public string RenderSpecification(string str)
        {
            return Md.RenderLineToHtml(str);
        }

        [Test]
        public void RenderForLinearTime()
        {
            const int textLength = 50000;
            const int factor = 100;
            var shortText = GenerateRandomMarkdownString(textLength);
            var longText = string.Join("", Enumerable.Repeat(shortText, factor));

            var timeOnShort = TimeIt(() => Md.RenderLineToHtml(shortText));
            var timeOnLong = TimeIt(() => Md.RenderLineToHtml(longText));

            Console.WriteLine($"On long text: {timeOnLong}ms");
            Console.WriteLine($"On short text: {timeOnShort}ms");
            Console.WriteLine($"Time factor: {(double)timeOnLong / timeOnShort}");
            Console.WriteLine($"Input size factor: {factor}");

            ((double) timeOnLong/timeOnShort).Should().BeLessOrEqualTo(factor + 2);
        }

        public long TimeIt(Action act)
        {
            act.Invoke();
            act.Invoke();
            act.Invoke();
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10; i++)
                act.Invoke();
            sw.Stop();
            return sw.ElapsedMilliseconds / 10;
        }

        public string GenerateRandomMarkdownString(int length, int seed=0)
        {
            var symbols = new[] {'_', '_', 'a', 'b', 'c', 'd', 'e', ' '};
            var random = new Random(seed);
            return string.Join("", Enumerable.Range(0, length).Select(_ => symbols[random.Next(symbols.Length)]));
        }
    }
}