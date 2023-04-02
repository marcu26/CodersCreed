import { Component } from '@angular/core';

@Component({
  selector: 'app-course-quizz',
  templateUrl: './course-quizz.component.html',
  styleUrls: ['./course-quizz.component.css']
})
export class CourseQuizzComponent {
  questions = [
    {
      id: 1,
      question: "What is the capital of France?",
      choices: ["London", "Paris", "Berlin", "Madrid"],
      answer: 1,
      selected: null
    },
    {
      id: 2,
      question: "What is the highest mountain in the world?",
      choices: ["Everest", "K2", "Makalu", "Lhotse"],
      answer: 0,
      selected: null
    }
  ];

  allQuestionsAnswered = false;
  quizCompleted = false;
  score = 0;

  startQuiz() {
    // Quiz started
  }

  selectAnswer() {
    // Check if all questions have been answered
    this.allQuestionsAnswered = this.questions.every(q => q.selected !== null);
  }

  submit() {
    // Calculate score
    this.score = this.questions.filter(q => q.selected === q.choices[q.answer]).length;
    // Quiz completed
    this.quizCompleted = true;
  }
}