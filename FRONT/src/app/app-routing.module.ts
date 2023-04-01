import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { RewardsComponent } from './rewards/rewards.component';

const routes: Routes = [
	{ path: "projects", component: ProjectsComponent, data: { animation: 'isLeft' } },
	{ path: "leaderboard", component: LeaderboardComponent, data: { animation: 'isRight' } },
	{ path: "rewards", component: RewardsComponent, data: { animation: 'rewards' } },
	{ path: "profile", component: ProfileComponent, data: { animation: 'profile' } },
	{ path: "login", component: LoginComponent },
	{ path: "**", component: LoginComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
