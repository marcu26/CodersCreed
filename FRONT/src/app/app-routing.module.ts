import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { RewardsComponent } from './rewards/rewards.component';
import { CoursePreviewComponent } from './course-preview/course-preview.component';

const routes: Routes = [
	{ path: "projects", component: ProjectsComponent },
	{ path: "leaderboard", component: LeaderboardComponent },
	{ path: "rewards", component: RewardsComponent },
	{ path: "profile", component: CoursePreviewComponent },
	{ path: "login", component: LoginComponent },
	{ path: "**", component: LoginComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
