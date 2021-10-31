using FluentAssertions;
using HomeExercises;
using NUnit.Framework;

namespace HomeExercisesTests
{
    public class ObjectComparison
    {
        [Test]
        [Description("�������� �������� ����")]
        [Category("ToRefactor")]
        public void CheckCurrentTsar()
        {
            var actualTsar = TsarRegistry.GetCurrentTsar();

            var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
                new Person("Vasili III of Russia", 28, 170, 60, null));

            // ���������� ��� �� ������������� Fluent Assertions.
            actualTsar.Should().BeEquivalentTo(expectedTsar, config
                => config.Excluding(member
                => member.SelectedMemberInfo.Name == nameof(expectedTsar.Id)));
        }

        [Test]
        [Description("�������������� �������. ����� � ���� ����������?")]
        public void CheckCurrentTsar_WithCustomEquality()
        {
            var actualTsar = TsarRegistry.GetCurrentTsar();
            var expectedTsar = new Person("Ivan IV The Terrible", 54, 170, 70,
                new Person("Vasili III of Russia", 28, 170, 60, null));

            // ����� ���������� � ������ �������? 
            //��������� ����� ������������, ��� ��������� Person ����� �������� ������������
            //��������� �������� ��� ��������� ��������
            //������ ��������, ���������, ��� ����� ������ ����
            //�� � ����� AreEqual, ���������� � Person, �������� ������� � ������ � �������
            Assert.True(AreEqual(actualTsar, expectedTsar));
        }

        private bool AreEqual(Person? actual, Person? expected)
        {
            if (actual == expected) return true;
            if (actual == null || expected == null) return false;
            return
                actual.Name == expected.Name
                && actual.Age == expected.Age
                && actual.Height == expected.Height
                && actual.Weight == expected.Weight
                && AreEqual(actual.Parent, expected.Parent);
        }
    }

    public class TsarRegistry
    {
        public static Person GetCurrentTsar()
        {
            return new Person(
                "Ivan IV The Terrible", 54, 170, 70,
                new Person("Vasili III of Russia", 28, 170, 60, null));
        }
    }
}