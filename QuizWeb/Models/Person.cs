namespace QuizWeb.Models
{
	public class Person
	{
		public int id { get; set; }
		public string fio { get; set; }

	}

	public class PersonScore
	{
		public int id { get; set; }
		public string fio { get; set; }

		public int count { get; set; }

	}

	public class Victorina
	{
		public int id { get; set; }
		public int personid { get; set; }
		public int count { get; set; }

	}
	public class Question
	{
		public int id { get; set; }
		public string questionText { get; set; }
	}

	public class Answer
	{
		public int id { get; set; }
		public int questionId { get; set; }
		public string answerText { get; set; }
		public bool isCorrect { get; set; }

	}
		public class AnswerQuestion
	{
		public int id { get; set; }
		public int questionId { get; set; }
		public string answerText { get; set; }
		public string questionText { get; set; }
		public string theme { get; set; }
		public bool isCorrect { get; set; }

	}

}
