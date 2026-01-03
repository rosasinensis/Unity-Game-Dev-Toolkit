using System;
using System.Collections.Generic;

public static class MatchUtil
{
    public static bool CanSatisfyAll<TReq, TItem>(IList<TReq> requirements, IList<TItem> items, Func<TReq, TItem, bool> matchCriteria)
    {

        // Recursively checks if items in a list meets items in another list, given criteria.

        // Example:
        // MatchUtil.CanSatisfyAll(recipeIngredients, playerIngredients, (rule, item) => rule.IsMatch(item);

        if (requirements.Count > items.Count)
        {
            // Fail if there are more requirements than items to check against.

            return false;
        }

        return RunMatch(0, requirements, items, new bool[items.Count], matchCriteria);
    }

    private static bool RunMatch<TReq, TItem>(int requirementIndex, IList<TReq> requirements, IList<TItem> items, bool[] used, Func<TReq, TItem, bool> matchCriteria)
    {

        if (requirementIndex >= requirements.Count)
        {
            // Reached the limit of the requirements, meaning it reached the end.

            return true;
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (!used[i] && matchCriteria(requirements[requirementIndex], items[i]))
            {
                // Criteria met. Consider item as 'used'.
                used[i] = true;

                if (RunMatch(requirementIndex + 1, requirements, items, used, matchCriteria))
                {

                    // RunMatch on the NEXT requirement.

                    return true;
                }

                // There was an end to the list where the requirement was NOT found in the items. The item's index is flagged as unused again, and the function tries again with the next item to see if the next item combination will be met instead.

                used[i] = false;
            }
        }

        return false;
    }
}
