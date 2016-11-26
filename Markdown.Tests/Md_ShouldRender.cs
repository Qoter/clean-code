using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Md_ShouldRender
    {
        public static IEnumerable<TestCaseData> SpecifictaionCases()
        {
            yield return
                new TestCaseData("Текст _окруженный с двух сторон_  одинарными символами подчерка должен помещаться в em")
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

            yield return new TestCaseData("[Должны поддерживаться ссылки](http://ссылка)")
                                 .Returns("<p><a src='http://ссылка'>Должны поддерживаться ссылки</a></p>")
                                 .SetName("Ссылки");


            yield return new TestCaseData("[внутри ссылок работает _курсив_ и __жирный__](http://ссылка)")
                                 .Returns( "<p><a src='http://ссылка'>внутри ссылок работает <em>курсив</em> и <strong>жирный</strong></a></p>")
                                 .SetName("Ссылки с курсивным и жирным выделением");

            yield return new TestCaseData("Если между двух строк есть пустая строка\r\n\r\nто эти строки в разных параграфах")
                                 .Returns("<p>Если между двух строк есть пустая строка</p><p>то эти строки в разных параграфах</p>")
                                 .SetName("Текст разбивается на параграфы");

            yield return new TestCaseData("Когда строка заканчивается на два пробела или более  \r\nто происходит перенос")
                                 .Returns("<p>Когда строка заканчивается на два пробела или более<br />то происходит перенос</p>")
                                 .SetName("Переносы строк");

            yield return new TestCaseData("Но переноса не происходит, \r\nесли строки идут друг за другом")
                                 .Returns("<p>Но переноса не происходит, \r\nесли строки идут друг за другом</p>")
                                 .SetName("Переноса не происходит, если строка не заканчивается пробелами");

            yield return new TestCaseData("#Есть заголовки\r\n###Разного уровня\r\nНо это #не заголовок")
                                 .Returns("<p><h1>Есть заголовки</h1>\r\n<h3>Разного уровня</h3>\r\nНо это #не заголовок</p>")
                                 .SetName("Заголовки");


        }

        [TestCaseSource(nameof(SpecifictaionCases))]
        public string RenderSpecification(string str)
        {
            return new Md(MdSettings.Default).RenderTextToHtml(str);
        }

        [TestCase("abc", ExpectedResult = "<p>abc</p>", TestName = "Один параграф, если нет пустых строк")]
        [TestCase("a\r\n\r\nb", ExpectedResult = "<p>a</p><p>b</p>", TestName = "Параграфы отделяются пустой строкой")]
        [TestCase("a\r\n   \r\nb", ExpectedResult = "<p>a</p><p>b</p>", TestName = "Строка считается пустой если она пробельная")]
        [TestCase("a\r\n\r\n\r\nb", ExpectedResult = "<p>a</p><p>b</p>", TestName = "Параграфы могут быть отделены несколькими пустыми строками")]
        public string SplitOnParagraphs(string text)
        {
            var renderer = new Md(MdSettings.Default);

            return renderer.RenderTextToHtml(text);
        }

        [Test]
        public void AddBaseCssClass_Setting()
        {
            var md = new Md(new MdSettings(cssClass: "test"));

            var renderedLine = md.RenderTextToHtml("_все_ теги __должны__ иметь класс [test](ссылка)");

            renderedLine.Should().Be("<p class='test'><em class='test'>все</em>" +
                                     " теги <strong class='test'>должны</strong> иметь класс " +
                                     "<a class='test' src='ссылка'>test</a></p>");
        }


        [Test]
        public void AddBaseUrl_SettingsWithBaseUrl()
        {
            var md = new Md(new MdSettings(new Uri("http://test")));

            var renderedLine = md.RenderTextToHtml("Ссылка от базового адреса [клац](test.html)");

            renderedLine.Should().Be("<p>Ссылка от базового адреса <a src='http://test/test.html'>клац</a></p>");
        }
    }
}