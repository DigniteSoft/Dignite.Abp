import { ABP } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { SetCurrentAppState } from '../actions/app.action';
import { App } from '../models/navigation';

@State<App.State>({
    name: 'DigniteNavigationState',
    defaults: { currentApp: {} } as App.State
})
@Injectable()
export class AppState {

    @Selector()
    static getCurrentAppState({ currentApp }: App.State): ABP.Route {
        return currentApp;
    }

    @Action(SetCurrentAppState)
    setCurrentAppState({ patchState }: StateContext<App.State>, { payload }: SetCurrentAppState) {
        patchState({ currentApp: payload });
    }
}
