import { ABP, RoutesService, TreeNode } from '@abp/ng.core';
import { Component, OnInit, TrackByFunction } from '@angular/core';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppState } from '../../states/app.state';

@Component({
    selector: 'dignite-routes',
    templateUrl: './routes.component.html',
    styles: ['.active { background: rgba(0, 0, 0, 0.04)!important; }'],
})
export class RoutesComponent implements OnInit {

    @Select(AppState.getCurrentAppState)
    currentApp$: Observable<TreeNode<ABP.Route>>;

    get navigations$() {
        return this.currentApp$.pipe(map(m => m.children));
    }

    trackByFn: TrackByFunction<TreeNode<ABP.Route>> = (_, item) => item.name;

    constructor(
        public readonly routesService: RoutesService,
    ) {
    }

    ngOnInit() {

    }

    isDropdown(node: TreeNode<ABP.Route>) {
        return !node?.isLeaf || this.routesService.hasChildren(node.name);
    }
}
