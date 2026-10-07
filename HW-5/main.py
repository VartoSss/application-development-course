from typing import List

class SolutionHuman:
    """
    Решение человека - Александров Никита РИМ-160950
    """
    @staticmethod
    def two_sum_not_optimal(nums, target):
        """
        Метод находит индексы двух чисел массива такие, что их сумма равна target.
        Алгоритмическая сложность O(n^2), где n - длина массива nums,
        так как два вложенных цикла в худшем случае пройдут 1 + 2 + ... + n операций,
        что равно по формуле арифметической прогрессии (1 + n / 2) * n,
        который оценивается в O(n ^ 2).
        Дополнительной памяти нет, оценка памяти O(1).

        Результат литкод: время 2119 ms, Beats 36.67%. Память 13.40 MB, Beats 18.55%
        """
        for i in range(len(nums)):
            for j in range(i + 1, len(nums)):
                if nums[i] + nums[j] == target:
                    return [i, j]
        return None

    @staticmethod
    def two_sum_optimal(nums, target):
        """
        Метод находит индексы двух чисел массива такие, что их сумма равна target.
        Сложность алгоритма амортизированно O(n), где n - длина массива nums,
        так как один цикл проходит массив - это линия, в каждой итерации происходит обращение к словарю за O(1)
        и/или добавление в словарь амортизированно тоже за O(1).
        Оценка по памяти O(n), так как выделяем дополнительный словарь, в котором окажутся все элементы массива.

        Результат литкод: время 3ms, Beats 57.23%. Память 13.32, Beats 18.55%

        Работает быстрее решения в лоб из-за того, что алгоритм со сложностью O(n) выполняет меньшее количество элементарных операций
        чем алгоритм со сложностью O(n^2) на достаточно больших входных данных, что и показывает результат тестов.
        """
        seen = dict()
        for i in range(len(nums)):
            second_num = target - nums[i]
            if second_num in seen:
                return [seen[second_num], i]
            if nums[i] not in seen:
                seen[nums[i]] = i
        return None


class SolutionAgent:
    """
    Решение, которое придумал агент по запросу "Напиши 2 решения: в лоб и оптимальное и напиши сложности".
    Идейно решения одинаковые с человеческими, оба решения проходят все тесты. Ошибки не поймано
    """
    def twoSumBruteForce(self, nums: List[int], target: int) -> List[int]:
        """
        Решение в лоб: перебираем все пары индексов.

        Время: O(n^2) — для каждого i проверяем все j > i.
        Память: O(1) — дополнительная память не зависит от n
        (не считая память под ответ).
        """
        n = len(nums)
        for i in range(n):
            for j in range(i + 1, n):
                if nums[i] + nums[j] == target:
                    return [i, j]
        return []

    def twoSum(self, nums: List[int], target: int) -> List[int]:
        """
        Оптимальное решение: один проход и словарь «значение -> индекс».

        Для каждого nums[i] ищем complement = target - nums[i].
        Если complement уже встречался, возвращаем его индекс и i.
        Иначе сохраняем nums[i] в словарь.

        Время: O(n) — один проход, поиск и вставка в словарь за O(1) в среднем.
        Память: O(n) — в словаре хранится до n элементов.
        """
        seen = {}
        for i, num in enumerate(nums):
            complement = target - num
            if complement in seen:
                return [seen[complement], i]
            seen[num] = i
        return []


def main():
    nums = [2, 7, 11, 15]
    target = 9

    solution_agent = SolutionAgent()
    print(solution_agent.twoSumBruteForce(nums, target))  # [0, 1]
    print(solution_agent.twoSum(nums, target))  # [0, 1]

    solution_human = SolutionHuman()
    print(solution_human.two_sum_not_optimal(nums, target))  # [0, 1]
    print(solution_human.two_sum_optimal(nums, target))  # [0, 1]


if __name__ == '__main__':
    main()