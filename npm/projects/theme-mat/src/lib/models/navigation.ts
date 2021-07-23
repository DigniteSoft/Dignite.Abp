import { ABP } from '@abp/ng.core';

// tslint:disable-next-line:no-namespace
export namespace App {
    export interface State {
        currentApp: ABP.Route;
    }
}
