using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.ExtendAndHelper.Extends
{
    public static class JTokenExtend
    {
        /// <summary>
        /// 需要验证的Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        /// <summary>
        /// 验证在一个token中是否包含另一个token（使用字符串模式匹配）
        /// </summary>
        /// <param name="token">期望包含某字段的token</param>
        /// <param name="tokeneq">需要验证的token</param>
        /// <returns></returns>
        public static bool TokenStrContains(this JToken token,JToken tokeneq)
        {
            var eqStr = tokeneq.ToString();

            if (token.HasValues) // token 是否有子token
            {
                foreach(var item in token.Children())
                {
                    if (item.ToString()== eqStr)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return token.ToString() == eqStr;
            }

        }


    }
}
