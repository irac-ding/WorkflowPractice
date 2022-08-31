using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkflowCore.Test.Model
{
    public class SagaDoModel
    {
        /// <summary>
        /// 容器内步骤id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 步骤的方法名反射,默认为"WorkflowCore.Primitives.Sequence, WorkflowCore"
        /// </summary>
        [Required]
        public string StepType { get; set; } = "WorkflowCore.Primitives.Sequence, WorkflowCore";
        /// <summary>
        /// 容器内下一个步骤id
        /// </summary>
        public string NextStepId { get; set; } = "";
        /// <summary>
        /// 补偿方法执行规则为：
        /// 1、最接近报错方法的回滚方法先执行，如此类推
        /// 2、当有onerror步骤时，回滚方法不执行
        /// </summary>
        public List<CompensateWithModel> CompensateWith { get; set; } = null;

        /// <summary>
        /// 发生错误后
        /// 例如
        /// WorkflowErrorHandling.Retry.ToString()
        /// </summary>
        public string ErrorBehavior { get; set; } = null;
        /// <summary>
        /// 发生错误后执行时间
        /// 例如："00:00:05"
        /// </summary>
        public string RetryInterval { get; set; } = null;
    }

}
