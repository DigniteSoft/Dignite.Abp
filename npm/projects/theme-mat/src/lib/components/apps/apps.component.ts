import { ABP, RoutesService, TreeNode } from '@abp/ng.core';
import { Component, OnInit, TrackByFunction } from '@angular/core';
import { Router } from '@angular/router';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { AppService } from '../../services/app.service';
import { AppState } from '../../states/app.state';

@Component({
    selector: 'dignite-apps',
    templateUrl: './apps.component.html'
})
export class AppsComponent implements OnInit {

    @Select(AppState.getCurrentAppState)
    currentApp$: Observable<TreeNode<ABP.Route>>;

    apps: ABP.Route[] = [];
    appLimitNumber = 6;

    trackByFn: TrackByFunction<TreeNode<ABP.Route>> = (_, item) => item.name;

    constructor(
        public readonly routesService: RoutesService,
        public appService: AppService,
        private router: Router
    ) {
    }

    ngOnInit(): void {
        this.apps = this.routesService.tree.filter(m => !m.invisible);
    }

    navigationByRoute(route: TreeNode<ABP.Route>) {
        const url = this.appService.getNavigateUrl(route);
        this.router.navigateByUrl(url);
    }
}
