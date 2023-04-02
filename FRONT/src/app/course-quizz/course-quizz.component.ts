import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-course-quizz',
  templateUrl: './course-quizz.component.html',
  styleUrls: ['./course-quizz.component.css']
})
export class CourseQuizzComponent {

  questions = [
    {
      id: 1,
      question: "int x=~1; What is the value of 'x'?",
      choices: ["1", "-1", "2", "-2"],
      answer: 3,
      selected: null
    },
    {
      id: 2,
      question: "What is the output of the following program?",
      choices: ["\\", "\\\"", "\"", "Compile error"],
      answer: 0,
      selected: null
    },
    {
      id: 3,
      question: "Which library function can convert an integer/long to a string?",
      choices: ["ltoa()", "ultoa()", "sprintf()", "None of the above"],
      answer: 0,
      selected: null
    },
    {
      id: 4,
      question: "Which of the following statement can be used to free the allocated memory?",
      choices: ["remove(var-name);", "free(var-name);", "vanish(var-name);", "erase(var-name);"],
      answer: 0,
      selected: null
    }

  ];

  allQuestionsAnswered = false;
  quizCompleted = false;
  score = 0;


  choosen_variant(question: any, answer: any) {
    question[question].selected = answer;
  }


  submit() {
    this.score = this.questions.filter(q => q.selected === q.choices[q.answer]).length;
    this.quizCompleted = true;
  }
}