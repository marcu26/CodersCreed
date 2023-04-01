import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { RewardsComponent } from './rewards/rewards.component';
import { CoursePreviewComponent } from './course-preview/course-preview.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'

const routes: Routes = [
	{ path: "projects", component: ProjectsComponent },
	{ path: "leaderboard", component: LeaderboardComponent },
	{ path: "rewards", component: RewardsComponent },
	{ path: "profile", component: ProfileComponent },
	{ path: "login", component: LoginComponent },
	{ path: "course", component: CoursePreviewComponent },
	{ path: "**", component: LoginComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
