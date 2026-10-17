class SolutionValidParentheses(object):
    def isValidHuman(self, s):
        """
        :type s: str
        :rtype: bool


        Решение работает за O(n), так как проходим по строке один раз,
        добавить в конец стека (даже на основе списка), как и взять элемент с конца - амортизировано O(1).
        
        Стек подходит для решения задачи, так как при правильной скобочной последовательности
        каждая закрывающая скобка строго должна иметь предыдущим элементом такую же открывающую 
        даже при условии вложенных скобочек - это механика LIFO. 
        """
        openers = "({["
        closers = ")]}"
        pairs = {")":"(", "]":"[", "}": "{"}
        stack = []
        for elem in s:
            if(elem in openers):
                stack.append(elem)
            else:
                if len(stack) == 0:
                    return False
                prev_element = stack.pop()
                if prev_element in closers or pairs[elem] != prev_element:
                    return False
                    
        return len(stack) == 0

    def isValidAgent(self, s):
        """
        :type s: str
        :rtype: bool

        How it works:
        Use a stack to track opening brackets.
        For each character:
        
        If it's an opening bracket ((, {, [), push it onto the stack.
        
        If it's a closing bracket (), }, ]), check whether the top of the stack holds its matching opening bracket. If not (or the stack is empty), the string is invalid.
        
        At the end, the stack must be empty — otherwise some opening bracket was never closed.

        Complexity:

        Time: O(n) — single pass over the string.
        Space: O(n) — worst case (e.g., "((((("), the stack holds all characters.
        """
        # Map each closing bracket to its matching opening bracket
        pairs = {')': '(', '}': '{', ']': '['}
        stack = []

        for char in s:
            if char in pairs:
                # Closing bracket: check top of stack
                if not stack or stack[-1] != pairs[char]:
                    return False
                stack.pop()
            else:
                # Opening bracket: push onto stack
                stack.append(char)

        # Valid only if no unmatched opening brackets remain
        return not stack


class SolutionMyQueueHuman(object):
    '''
    Очередь на двух стках (для простоты будем назвать один - вводным, а второй - выходным):
    1. Добавление - это всегда пуш в вводный стек
    2. Для взятия в стеке элементы нужно развернуть, чтобы первый вошедший стал первым на выход - поэтому:
        
        2.1 Если выходной стек пуст - по очереди достаем сверху каждый элемент входного массива и кладем его в выходной,
        тем самым разворачивая эллементы.
        
        2.2 Выходной стек не пуст (в том числе и после действия 2.1) - доставай из него - там элементы уже выходят в правильном порядке
    '''
    def __init__(self):
        self.in_stack = []
        self.out_stack = []

    def push(self, x):
        self.in_stack.append(x)

    def pop(self):
        self.peek()
        return self.out_stack.pop()

    def peek(self):
        if(len(self.out_stack) != 0):
            return self.out_stack[-1]
        else:
            while(len(self.in_stack) != 0):
                self.out_stack.append(self.in_stack.pop())
            return self.out_stack[-1]

    def empty(self):
        return len(self.in_stack) == 0 and len(self.out_stack) ==0