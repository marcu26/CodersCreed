import { Component } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-course-preview',
  templateUrl: './course-preview.component.html',
  styleUrls: ['./course-preview.component.css'],
  animations: [
    trigger('toggleAccordion', [
      state('collapsed', style({ height: '0' })),
      state('expanded', style({ height: '*' })),
      transition('collapsed <=> expanded', animate('300ms ease'))
    ])
  ]
})
export class CoursePreviewComponent {
  numberOfItems = 20;
  panelOpenState = false;
  items = [
    {
      src: "http://homomorphicencryption.org/wp-content/uploads/2018/11/HomomorphicEncryptionStandardv1.1.pdf",
      title: "Curs"
    },
    {
      src: "http://homomorphicencryption.org/wp-content/uploads/2018/11/HomomorphicEncryptionStandardv1.1.pdf",
      title: "Curs"
    },
    {
      src: "http://homomorphicencryption.org/wp-content/uploads/2018/11/HomomorphicEncryptionStandardv1.1.pdf",
      title: "Curs"
    }
  ];
  isExpendit: boolean[] = new Array(20).fill(false);
  expendIt(value: number) {

    this.isExpendit[value] = !this.isExpendit[value];
  }
}
