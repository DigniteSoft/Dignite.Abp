import { FieldCustomizing } from '../../models/field-customizing';
import { TextBoxViewComponent } from './textbox-view.component';

/**
 * 简单form组件
 */
const SIMPLE_TEXT_PROVIDERS: FieldCustomizing.FormProvider[] = [{
    displayName: '简单组件-用于解析表单值',
    name: 'simple-text',
    useType: 'view',
    component: TextBoxViewComponent
}, {
    displayName: '简单组件-用于表单填写',
    name: 'simple-text',
    useType: 'form',
    component: TextBoxViewComponent
}];

export default SIMPLE_TEXT_PROVIDERS;
