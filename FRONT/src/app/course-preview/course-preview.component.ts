import { Component } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-course-preview',
  templateUrl: './course-preview.component.html',
  styleUrls: ['./course-preview.component.css'],  animations: [
    trigger('toggleAccordion', [
      state('collapsed', style({ height: '0' })),
      state('expanded', style({ height: '*' })),
      transition('collapsed <=> expanded', animate('300ms ease'))
    ])
  ]
})
export class CoursePreviewComponent {
  numberOfItems = 20;
  isExpendit: boolean[] = new Array(20).fill(false);
  expendIt(value:number){
    
    this.isExpendit[value]=!this.isExpendit[value];
  }
}
