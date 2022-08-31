using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkflowCore.Test.Model
{

    public class ActivitySetpInputModel
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动参数
        /// </summary>
        [Required]
        public string Parameters { get; set; }
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
