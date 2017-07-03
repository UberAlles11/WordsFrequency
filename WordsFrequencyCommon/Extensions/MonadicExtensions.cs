using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsFrequency.Common.Extensions
{
    public static class MonadicExtensions
    {
        #region With
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o);
        }
        #endregion

        #region Return
        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue) where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }
        #endregion

        #region If
        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }
        #endregion

        #region Unless
        public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? null : o;
        }
        #endregion

        #region Do
        public static TInput Do<TInput>(this TInput o, Action<TInput> action)  where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }
        #endregion

        #region Do
        public static TInput Do<TInput>(this TInput o, Func<TInput, bool> evaluator, Action<TInput> action) where TInput : class
        {
            if (o == null) return null;
            if(evaluator(o)) action(o);
            return o;
        }
        #endregion

    }
}
