
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



	public class BaseEvent 
	{

		Dictionary<Type, List<BaseResponse>> responsePool;

		public BaseEvent()
		{

		}

		public void AddResponse(BaseResponse response)
		{
			Type type = response.GetType();
			if(!responsePool.ContainsKey(type))
			{
				responsePool.Add(type, new List<BaseResponse>());
			}

			responsePool[type].Add(response);
		}

		public BaseResponse[] GetResponsesOfType(Type type)
		{
			if(responsePool.ContainsKey(type))
			{
				return responsePool[type].ToArray();
			}

			return null;
		}
	}

	abstract public class BaseResponse
	{


		public BaseResponse()
		{

		}
	}
	

