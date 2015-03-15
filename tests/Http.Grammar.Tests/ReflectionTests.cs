namespace Http.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SLANG;

    [TestClass]
    public class ReflectionTests
    {
        private static Assembly Assembly;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Assembly = Assembly.Load("Http.Grammar");
        }

        [TestMethod]
        public void AllElementsShouldBePublic()
        {
            var allTypes = Assembly.GetTypes();
            var elementTypes = allTypes.Where(type => typeof(Element).IsAssignableFrom(type)).ToList();
            foreach (var type in elementTypes)
            {
                Assert.IsTrue(type.IsPublic, "typeof({0}).IsPublic", type.FullName);
            }
        }

        [TestMethod]
        public void AllLexersShouldBePublic()
        {
            var allTypes = Assembly.GetTypes();
            var elementTypes = allTypes.Where(type => typeof(Lexer<>).IsAssignableFrom(type)).ToList();
            foreach (var type in elementTypes)
            {
                Assert.IsTrue(type.IsPublic, "typeof({0}).IsPublic", type.FullName);
            }
        }

        [TestMethod]
        public void AllLexersShouldHaveParameterlessConstructors()
        {
            var allTypes = Assembly.GetTypes();
            var elementTypes = allTypes.Where(type => type.GetInterfaces().Where(i => i.IsGenericType).Select(i => i.GetGenericTypeDefinition()).Contains(typeof(ILexer<>))).ToList();
            foreach (var type in elementTypes)
            {
                var constructor = type.GetConstructor(Type.EmptyTypes);
                Assert.IsNotNull(constructor, "typeof({0}).GetConstructor(Type.EmptyTypes)", type.FullName);
            }
        }
    }
}