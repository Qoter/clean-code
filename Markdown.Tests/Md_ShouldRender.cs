using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Md_ShouldRender
    {
        public static IEnumerable<TestCaseData> SpecifictaionCases()
        {
            yield return new TestCaseData("Текст _окруженный с двух сторон_  одинарными символами подчерка должен помещаться в em")
                                 .Returns("<p>Текст <em>окруженный с двух сторон</em>  одинарными символами подчерка должен помещаться в em</p>")
                                 .SetName("Курсив");

            yield return new TestCaseData("\\_Вот это\\_ не выделяется em")
                                 .Returns("<p>_Вот это_ не выделяется em</p>")
                                 .SetName("Экранирование");

            yield return new TestCaseData("__Двумя символами__ — должен становиться жирным с помощью тега strong")
                                 .Returns("<p><strong>Двумя символами</strong> — должен становиться жирным с помощью тега strong</p>")
                                 .SetName("Жирный");

            yield return new TestCaseData("Внутри __двойного выделения _одинарное_ тоже__ работает")
                                 .Returns("<p>Внутри <strong>двойного выделения <em>одинарное</em> тоже</strong> работает</p>")
                                 .SetName("Курсив внутри жирного");

            yield return new TestCaseData("Но не наоборот — внутри _одинарного __двойное__ не работает_.")
                                 .Returns("<p>Но не наоборот — внутри <em>одинарного __двойное_</em> не работает_.</p>")
                                 .SetName("Жирный внутри курсива - не работает");

            yield return new TestCaseData("Подчерки внутри текста c цифрами _12_3 __12__3 не считаются выделением")
                                 .Returns("<p>Подчерки внутри текста c цифрами _12_3 __12__3 не считаются выделением</p>")
                                 .SetName("Подчерки внутри цифр - не выделение");

            yield return new TestCaseData("Подчерки внутри слов не считаются выделением _h_e_l_l_o_world")
                                 .Returns("<p>Подчерки внутри слов не считаются выделением _h_e_l_l_o_world</p>")
                                 .SetName("Подчерки внутри слов - не выделение");

            yield return new TestCaseData("__непарные _символы не считаются выделением")
                                 .Returns("<p>__непарные _символы не считаются выделением</p>")
                                 .SetName("Непарные симовлы - не выделение");


            yield return new TestCaseData("Эти_ подчерки_ не считаются выделением ")
                                 .Returns("<p>Эти_ подчерки_ не считаются выделением </p>")
                                 .SetName("Выделение не начинается, если за подчерком пробел");


            yield return new TestCaseData("_эти _подчерки _не считаются_ окончанием выделения")
                                 .Returns("<p><em>эти _подчерки _не считаются</em> окончанием выделения</p>")
                                 .SetName("Выделение не заканчивается, если перед подчерком пробел");
        }

        [TestCaseSource(nameof(SpecifictaionCases))]
        public string RenderSpecification(string str)
        {
            Console.WriteLine(Math.Sin(4));


            return Md.RenderLineToHtml(str);
        }
    }
}