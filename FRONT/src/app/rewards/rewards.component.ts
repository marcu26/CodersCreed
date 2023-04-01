import { Component } from '@angular/core';
import { event } from 'jquery';
import { RewardDialogComponent } from '../reward-dialog/reward-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-rewards',
  templateUrl: './rewards.component.html',
  styleUrls: ['./rewards.component.css']
})
export class RewardsComponent {
  button = [0, 0, 0, 0, 0, 0, 0, 0]
  matrix: Array<any> = [];

  ngOnInit() {
    this.getMatrix(5);
  }

  constructor(public dialog: MatDialog) { }

  getMatrix(size: number) {
    for (let i = 0; i < size; i++) {
      this.matrix.push(this.button.slice(i * size, (i + 1) * size));
    }
  }

  openDialog(reward: any) {
    this.dialog.open(RewardDialogComponent, {
      width: "20%",
      height: "30%",
      data: {
        name: 'Reward name',
        description: 'Reward description',
        id: 'reward id'
      },
    }).afterClosed().subscribe(val => { });
  }

}
