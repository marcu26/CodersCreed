import { Component, OnInit } from '@angular/core';
import { LeaderboardService } from '../api/leaderboard.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {
  colors = ['#FF9999', '#99FFB6', '#FD99FF', '#99DAFF']
  items: any
  length = 50;
  pageSize = 5;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];
  index = -1;

  constructor(private leaderboardService: LeaderboardService) {
  }

  ngOnInit(): void {
    this.get_leaderboard()
  }

  changeDisplayed(index: any) {
    this.index = index;
  }

  get_leaderboard() {
    this.leaderboardService.get_leaderboard(this.pageIndex * this.pageSize, this.pageSize).subscribe({
      next: (response) => {
        this.items = response.data;
        this.index = 0;
        this.length = response.recordTotal;
      },
      error: (error) => {
        console.log(error)
      }
    })
  }
}
