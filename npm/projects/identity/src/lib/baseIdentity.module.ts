import { ModuleWithProviders, NgModule } from "@angular/core";
import { REPLACE_COMPONENTS_PROVIDER } from "./providers";

@NgModule({
    imports: [

    ]
})
export class IdentityModule {
    static forRoot(): ModuleWithProviders<IdentityModule> {
        return {
            ngModule: IdentityModule,
            providers: [
                REPLACE_COMPONENTS_PROVIDER
            ]
        };
    }
}
