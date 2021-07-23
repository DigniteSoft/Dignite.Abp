import { ABP, PermissionService, RoutesService, TreeNode } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { NavigationEnd } from '@angular/router';
import { Store } from '@ngxs/store';
import { SetCurrentAppState } from '../actions/app.action';

@Injectable({
    providedIn: 'root'
})
export class AppService {

    constructor(
        private store: Store,
        private routeService: RoutesService,
        private permissionService: PermissionService
    ) {
    }

    /**
     * 根据点击路由查找可跳转的url
     */
    getNavigateUrl(route: TreeNode<ABP.Route>): string {
        const routes = this.getRoutes(route.children, []);
        for (const item of routes) {
            if (this.getGrantedPolicy(item.requiredPolicy) && item.path) {
                return item.path;
            }
        }
        return route.path || '';
    }


    /**
     * 监听路由变更事件
     * @param event angular路由事件
     */
    setCurrentApp(event: NavigationEnd) {
        let url = event.url;
        if (url === '/') {
            url = event.urlAfterRedirects;
        }

        console.log(this.routeService.flat);

        const route = this.routeService.find(m => m.path === url.substring(0, url.indexOf('?') != -1 ? url.indexOf('?') : url.length));
        const parentRouteName = this.getParentRoute(route);
        const parentRoute = this.routeService.find(m => m.name === parentRouteName);

        // 如果不可见
        if (parentRoute.invisible) {
            return false;
        }
        this.store.dispatch(new SetCurrentAppState(parentRoute));
    }

    /**
     * 判断路由是否授权
     * @param item item
     */
    getRouteGranted(item: TreeNode<ABP.Route>): boolean {

        if (item.invisible) {
            return false;
        }

        /**
         * 如果没有权限，且无子集
         * 如果有权限，且无子集
         */
        if (
            (!item.requiredPolicy || item.requiredPolicy && this.getGrantedPolicy(item.requiredPolicy) && item.path) &&
            (item.children === undefined || !item.children.length)
        ) {
            return true;
        }

        const routes = this.getRoutes(item.children, []);
        for (const route of routes) {
            if (route.requiredPolicy) {
                if (this.getGrantedPolicy(route.requiredPolicy)) {
                    return true;
                }
            } else {
                return true;
            }
        }

        return false;
    }

    /**
     * 获取最小级路由
     * @param routes 根据路由子数据取最小级数据
     * @param newRoutes 处理过程变量
     */
    getRoutes(routes: TreeNode<ABP.Route>[], newRoutes: TreeNode<ABP.Route>[]) {
        if (routes && routes.length) {
            for (const route of routes) {
                if (route.children && route.children.length) {
                    this.getRoutes(route.children, newRoutes);
                } else {
                    newRoutes.push(route);
                }
            }
        }
        return newRoutes;
    }

    /**
     * 获取顶级route
     * @param route 当前路由
     * @returns 匹配的顶级路由
     */
    private getParentRoute(route: TreeNode<ABP.Route>): string {
        return route && route.parent ? this.getParentRoute(route.parent) : route.name;
    }

    private getGrantedPolicy(requiredPolicy: string): boolean {
        return this.permissionService.getGrantedPolicy(requiredPolicy);
    }

}
