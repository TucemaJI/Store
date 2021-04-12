using Store.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Mappers
{
    public abstract class BaseMapper<TFirst, TSecond>
    {
        public abstract TFirst Map(TSecond element);
        public abstract TSecond Map(TFirst element);

        public List<TFirst> Map(List<TSecond> elements, Action<TFirst> callback = null)
        {
            var objectCollection = new List<TFirst>();
            if (elements is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.EMPTY_COLLECTION });
            };
            foreach (TSecond element in elements)
            {
                TFirst newObject = Map(element);
                if (newObject != null)
                {
                    callback?.Invoke(newObject);
                    objectCollection.Add(newObject);
                }
            }
            return objectCollection;
        }

        public List<TSecond> Map(List<TFirst> elements, Action<TSecond> callback = null)
        {
            var objectCollection = new List<TSecond>();

            if (elements is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.EMPTY_COLLECTION });
            };
            foreach (TFirst element in elements)
            {
                TSecond newObject = Map(element);
                if (newObject != null)
                {
                    callback?.Invoke(newObject);
                    objectCollection.Add(newObject);
                }
            }
            return objectCollection;
        }
    }
}
