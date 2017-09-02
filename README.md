# Advanced Algorithms

Various important computer science algorithms generically implemented in C#

<a href="https://ci.appveyor.com/project/justcoding121/advanced-algorithms">![Build Status](https://ci.appveyor.com/api/projects/status/9xpcp4m87max2066?svg=true)</a>


## Data Structures

### List

- [X] Array List (dynamic array) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/List/ArrayList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Lists/ArrayList_Tests.cs))
- [X] Skip List ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/List/SkipList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Lists/SkipList_Tests.cs))

### Hash Set

- [X] HashSet (using [Separate Chaining](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/SeparateChainingHashSet.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/OpenAddressHashSet.cs)) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/HashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/HashSet/HashSet_Tests.cs))
- [X] Tree HashSet ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/HashSet/TreeHashSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/HashSet/TreeHashSet_Tests.cs))

### Hash Dictionaries

- [X] Dictionary (using [Separate Chaining](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/SeparateChainingDictionary.cs) optionally [Open Address Linear Probing](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/OpenAddressDictionary.cs)) ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/Dictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Dictionary/Dictionary_Tests.cs))
- [X] Tree Dictionary ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Dictionary/TreeDictionary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Dictionary/TreeDictionary_Tests.cs))

### Stack

- [X] Stack ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Stack/Stack.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Stack_Tests.cs))

### Queue

- [X] Queue ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Queue/Queue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Queue_Tests.cs))

#### Priority Queue

- [X] Min Priority Queue ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Queue/PriorityQueue/Min/PriorityQueue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/PriorityQueue_Tests.cs))
 
### Linked List

- [X] Singly linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/SinglyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/SinglyLinkedList_Tests.cs))
- [X] Doubly linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/DoublyLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/DoublyLinkedList_Tests.cs))
- [X] Circular linked list ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/LinkedList/CircularLinkedList.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/LinkedList/CircularLinkedList_Tests.cs))

### Heap

#### Min

- [X] Binary Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/BMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/BMinHeap_Tests.cs))
- [X] d-ary Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/d-aryMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/D-aryHeap_Tests.cs))
- [X] Binomial Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/BinomialMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/BinomialHeap_Tests.cs))
- [X] Fibornacci Min Heap ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/FibornacciMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/FibornacciHeap_Tests.cs))
- [X] Pairing Min Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Heap/Min/PairingMinHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/PairingHeap_Tests.cs))

Note: Among the implementations here in practice, with the exclusion of DecrementKey operation regular Binary Heap & d-ary Heap outperforms other in theory superiors. Of course because it don't have pointer juggling overhead and hey arrays are faster!

#### Max 

- [X] Binary Max Heap ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Heap/Max/BMaxHeap.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Heap/BMaxHeap_Tests.cs))

### Tree

- [X] Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/Tree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/Tree_Tests.cs))
- [X] Binary Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/BinaryTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BinaryTree_Tests.cs))

#### Binary Search Trees

- [X] Binary Search Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/BST.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BST_Tests.cs))
- [X] AVL Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/AvlTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/AVLTree_Tests.cs))
- [X] Red Black Tree ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Tree/RedBlackTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/RedBlackTree_Tests.cs))
- [X] Splay Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/SplayTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SplayTree_Tests.cs))
- [X] Treap Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/TreapTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/TreapTree_Tests.cs))

#### B Trees

- [X] B Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/BTree_Tests.cs))
- [X] B+ Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/B%2BTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/B%2BTree_Tests.cs))

#### Queryable Trees

- [X] Multi-dimensional Interval Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/IntervalTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/IntervalTree_Tests.cs)) using multi-level Red-black tree
- [X] Multi-dimensional Kd Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/K_DTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/KdTree_Tests.cs)) for range and nearest neigbour queries
- [X] Multi-dimensional Range Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/RangeTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/RangeTreeTests.cs))
- [X] Segment Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/SegmentTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SegmentTree_Tests.cs))
- [X] Binary Indexed Tree (Fenwick tree) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/FenwickTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/FenwickTree_Tests.cs))

#### LookUp Trees

- [X] Prefix Tree (Trie) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/Trie.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/Trie_Tests.cs))
- [X] Suffix Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/SuffixTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/SuffixTree_Tests.cs))
- [X] Ternary Search Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/TernarySearchTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/TernarySearchTree_Tests.cs))

#### Parse Trees

- [X] Expression Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Tree/ExpressionTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Tree/ExpressionTree_Tests.cs))

#### Set

- [X] Disjoint Set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Set/DisJointSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Set/DisJointSet_Tests.cs))
- [X] Sparse Set ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Set/SparseSet.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Set/SparseSet_Tests.cs))
- [X] Bloom Filter ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Set/BloomFilter.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DataStructures/Set/BloomFilter_Tests.cs))

### Graph

#### Adjacency List
- [X] DiGraph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/DiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/DiGraph_Tests.cs))
- [X] Graph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/Graph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/Graph_Tests.cs))
- [X] Weighted DiGraph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/WeightedDiGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/WeightedDiGraph_Tests.cs))
- [X] Weighted Graph ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DataStructures/Graph/AdjacencyList/WeightedGraph.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DataStructures/Graph/AdjacencyList/WeightedGraph_Tests.cs))

# Algorithms

## Graph Algorithms

### Articulation Points

- [X] Tarjan's Articulation Points Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ArticulationPoint/TarjansArticulationFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ArticulationPoints/TarjansArticulation_Tests.cs))

### Bridges

- [X] Tarjan's Bridge Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Bridge/TarjansBridgeFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Bridge/TarjansBridge_Tests.cs))

### Connectivity

- [X] Kosaraju's Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/KosarajuStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/KosarajuStronglyConnected_Tests.cs))
- [X] Tarjan's Strongly Connected Component Finder ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/TarjansStronglyConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/TarjansStronglyConnected_Tests.cs))
- [X] Tarjan's BiConnected Graph Tester ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Connectivity/TarjansBiConnected.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Connectivity/TarjansBiConnected_Tests.cs))

### Coloring

- [X] M-Coloring ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Coloring/MColorer.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Coloring/MColoring_Tests.cs))

### Cover

- [X] Min Vertex Cover ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Cover/MinVertexCover.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Cover/MinVertexCover.cs))

### Max Flow

- [X] Ford-Fulkerson algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/FordFulkerson.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/FordFulkerson_Tests.cs))
- [X] Edmonds Karp's improvement ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/EdmondsKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/EdmondsKarp_Tests.cs)) on Ford-Fulkerson algorithm
- [X] Push Relabel algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Flow/PushRelabel.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Flow/PushRelabel_Tests.cs))

### Shortest Path

- [X] Bellman-Ford algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Bellman-Ford.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/BellmanFord_Tests.cs))
- [X] Dijikstra's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Dijikstra.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/Dijikstras_Tests.cs)) using Fibornacci Heap.
- [X] Floyd-Warshall algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Floyd-Warshall.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/FloydWarshall_Tests.cs))
- [X] Johnson's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/ShortestPath/Johnsons.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/ShortestPath/Johnson_Tests.cs))

### Matching

- [X] Max Bipartite Matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Matching/BiPartiteMatching.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Matching/BiPartiteMatching_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm 
- [X] Hopcroft Karp algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Matching/HopcroftKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Matching/HopcroftKarp_Tests.cs))

### Cut

- [X] Minimum Cut ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Cut/MinimumCut.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Cut/MinCut_Tests.cs)) using Edmonds Karp's improved Ford Fulkerson Max Flow algorithm

### Search

- [X] Depth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/DepthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/DepthFirst_Tests.cs))
- [X] Breadth First ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/BreadthFirst.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/BreadthFirst_Tests.cs))
- [X] Bi-Directional ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/Search/BiDirectional.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/Search/BiDirectional_Tests.cs))

### Topological Sort

- [X] Depth First Method ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/TopologicalSort/DepthFirstTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/TopologicalSort/DepthFirstTopSort_Tests.cs))
- [X] Kahn's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/TopologicalSort/KahnTopSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/TopologicalSort/KahnTopSort_Tests.cs))

### Minimum Spanning Tree

- [X] Kruskal's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/MinimumSpanningTree/Kruskals.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/MinimumSpanningTree/Kruskals_Test.cs)) using Merge Sort and Disjoint Set
- [X] Prim's algorithm ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/GraphAlgorithms/MinimumSpanningTree/Prims.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/GraphAlgorithms/MinimumSpanningTree/Prims_Test.cs)) using Fibornacci Heap

## String

- [X] Permutation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Permutation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Permutate.cs))
- [X] Manacher's algorithm for linear time longest Palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/ManachersPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Manachers_Tests.cs))

### Pattern Matching

- [X] Rabin-Karp string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/RabinKarp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/RabinKarp_Tests.cs))
- [X] Knuth–Morris–Pratt (KMP) string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/KMP.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/KMP_Tests.cs))
- [X] Z algorithm for string search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/String/Search/ZAlgorithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/String/Search/Z_Tests.cs))

## Compression

- [X] Huffman Coding ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Compression/HuffmanCoding.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Compression/HuffmanCoding_Tests.cs)) using Fibornacci Min Heap

## Sorting and Searching

- [X] Median of stream of numbers ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/MedianStream.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/MedianStream_Tests.cs))
- [X] Array in zig-zag fashion ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/ZigZagOrderer.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/ZigZag_Tests.cs))
- [X] Binary Search ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Search/BinarySearch.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Search/BinarySearch_Tests.cs))
- [X] Search on almost sorted array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Search/SearchAlmostSorted.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Search/SearchAlmostSorted_Tests.cs))
- [X] Sort an almost sorted array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/SortAlmostSorted.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/SortAlmostSorted_Tests.cs))

### Sorting Algorithms

- [X] Bubble Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/BubbleSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/BubbleSort_Tests.cs))
- [X] Insertion Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/InsertionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/InsertionSort_Tests.cs))
- [X] Selection Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/SelectionSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/SelectionSort_Tests.cs))
- [X] Shell Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/ShellSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/ShellSort_Tests.cs))
- [X] Tree Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/TreeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/TreeSort_Tests.cs))
- [X] Quick Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/QuickSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/QuickSort_Tests.cs))
- [X] Heap Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/HeapSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/HeapSort_Tests.cs))
- [X] Merge Sort ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/Sorting/MergeSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/MergeSort_Tests.cs))
- [X] Bucket Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/BucketSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/BucketSort_Tests.cs))
- [X] Radix Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/RadixSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/RadixSort_Tests.cs))
- [X] Counting Sort ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Sorting/CountingSort.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Sorting/CountingSort_Tests.cs))

Note: On a decent desktop, in given implementations here for +ive random input integers, the clear winner is counting sort (~0.6 seconds to sort 1 million integers) followed by Radix Sort. Merge Sort, Heap Sort, Quick Sort & Bucket Sort are all winners for +ive & -ive random integer inputs with (~1 seconds to sort 1 million integers). Tree sort has pointer juggling overhead on backing Red-Black Tree, so performs 8 times slower than Merge Sort in practice. Bubble Sort, Insertion Sort, Selection Sort & Shell Sort are among the worst for random input as you might have guessed.

## Numerical Methods

- [X] kth Smallest ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/KthSmallest.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/KthSmallest_Tests.cs))
- [X] Check Primality ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/PrimeTester.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/Primality_Tests.cs))
- [X] Generate Primes using Sieve of Eratosthenes ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/PrimeGenerator.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/PrimeGenerator_Tests.cs))
- [X] Fast Exponentiation ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/NumericalMethods/Exponentiation.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/NumericalMethods/Exponentiation_Tests.cs))

## Classic Dynamic Programming Problems

All are top down solutions with memoization technique.

### Matrix

- [X] Max submatrix ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/MaxSubMatrix.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/MaxSubMatrix_Tests.cs))
- [X] Min cost matrix path ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/MinCostMatrixPath.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/MatrixMinCost_Tests.cs))
- [X] Chain matrix multiplication ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/ChainMultiplication.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/ChainMultiplication_Tests.cs))
- [X] Max 1s Rectangle in matrix ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/Max1sRectangle.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/Max1sRectangle_Tests.cs))
- [X] Max 1s Square in matrix ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/Max1sSquare.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/Max1sSquare_Tests.cs))
- [X] Max subsquare with X sides in matrix ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Matrix/MaxXSideSubSquare.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Matrix/MaxXSideSubSquare_Tests.cs))

### Counting

- [X] Count bool parenthesization ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/CountBoolParenthesization.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/BoolParenthesis_Tests.cs))
- [X] Count decoding ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/CountDecodings.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/CountDecodings_Test.cs))
- [X] Dice throw ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/DiceThrow.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/DiceThrow_Tests.cs))
- [X] Count possible binary tree from a preorder sequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/CountBinaryTree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/CountBinaryTree_Tests.cs))
- [X] Ways to cover a distance ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/WaysToCoverDistance.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/WaysToCover_Tests.cs))
- [X] Staircase problem in Fibornacci Series ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Count/StairCaseProblem.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Count/StairCaseProblem_Tests.cs))

### Maximizing

- [X] Knapsack problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/KnackSackProblems.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/KnackSackProblems_Tests.cs))
- [X] Max sum subsequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/MaxSumSubSequence.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/MaxSumSequence_Tests.cs))
- [X] Max increasing sum sequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/MaxSumIncreasingSequence.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/MaxSumIncreasingSequence_Tests.cs))
- [X] Max profit buy/sell stocks in K transactions ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/MaxProfitKTransactions.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/MaxProfitKTransactions_Tests.cs))
- [X] Box Stacking ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/BoxStacking.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/BoxStacking_Tests.cs))
- [X] Building Bridges ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/BuildingBridges.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/BuildingBridges_Tests.cs))
- [X] Burst Balloon ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/BurstBalloon.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/BurstBalloon_Tests.cs))
- [X] Cutting Rod ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/CuttingRod.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/CuttingRod_Tests.cs))
- [X] Print Max A's ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/PrintMaxAs.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/PrintMaxAs_Tests.cs))
- [X] Weighted Job Scheduling ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/WeightedJobScheduling.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/WeightedJobScheduling_Tests.cs))
- [X] Longest Chain ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Maximizing/LongestChain.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Maximizing/LongestChain_Tests.cs))

### Minimizing

- [X] Coin change problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/CoinChangeProblems.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/CoinChangeProblems_Tests.cs))
- [X] Assembly line scheduling ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/AssemblyLineScheduling.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/AssemblyLineScheduling_Tests.cs))
- [X] Min Egg drop problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/MinEggDrop.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/MinEggDrop_Tests.cs))
- [X] Min edit distance ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/MinEditDistance.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/MinEditDistance_Tests.cs))
- [X] Min array jumps ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/MinArrayJumps.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/MinArrayJumps_Tests.cs))
- [X] Travelling Salesman Problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Minimizing/TravellingSalesman.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Minimizing/TravellingSalesman_Tests.cs))

### Palindrome

- [X] Longest palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Palindrome/LongestPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Palindrome/LongestPalindrome_Tests.cs))
- [X] Shortest palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Palindrome/ShortestPalindrome.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Palindrome/ShortestPalindrome_Tests.cs))
- [X] Palindrome min cut (Min Partitioning) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Palindrome/PalindromeMinCut.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Palindrome/PalindromeMinCut_Tests.cs))
- [X] Min deletion to get a palindrome ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Palindrome/PalindromeMinDeletion.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Palindrome/PalindromeMinDeletion_Tests.cs))

### Sequence

- [X] Balanced partition ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sequence/BalancedPartition.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sequence/BalancedPartition_Tests.cs))
- [X] Distinct binary strings ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sequence/DistinctBinaryString.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sequence/DistinctBinaryString_Tests.cs))
- [X] Longest common subsequence  ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sequence/LongestCommonSubSequence.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sequence/LongestCommonSubSequence_Tests.cs))
- [X] Longest increasing subsequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sequence/LongestIncreasingSubSequence.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sequence/LongestIncreasingSubSequence_Tests.cs))
- [X] Longest bitonic sequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sequence/LongestBitonicSequence.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sequence/LongestBitonicSequence_Tests.cs))

### Sum

- [X] Subset sum ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Sum/SubSetSum.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Sum/SubSetSum_Tests.cs))

### String

- [X] Wild card matching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/String/WildCardMatching.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/String/WildCardMatching_Tests.cs))
- [X] String interleaving ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/String/StringInterleaving.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/String/StringInterleaving_Tests.cs))
- [X] Text Justification ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/String/TextJustification.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/String/TextJustification_Tests.cs))
- [X] Word Break problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/String/WordBreakProblem.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/String/WordBreak_Tests.cs))


### Other

- [X] Tower of hanoi ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/DynamicProgramming/TowerOfHanoi.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/TowerOfHanoi_Tests.cs))
- [X] Fibonacci number generator ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/Fibornacci.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/Fibornacci_Tests.cs))
- [X] Optimal game strategy ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/OptimalGameStrategy.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/OptimalGameStrategy_Tests.cs))
- [X] Optimal Binary Search Tree ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DynamicProgramming/OptimalBST.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DynamicProgramming/OptimalBST_Tests.cs))

## Bit Algorithms

- [X] Some common bit hacks ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/BitHacks.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/BitHacks_Tests.cs))
- [X] Int to Binary string ([Implementation](https://github.com/justcoding121/Algorithm-Sandbox/blob/master/Algorithm.Sandbox/BitAlgorithms/IntToBinary.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/IntToBinary_Tests.cs))
- [X] Base conversion ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/BaseConversion.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/BaseConversion_Tests.cs))
- [X] Find the element that appears once ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/FindUniqueElement.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/FindUniqueElement_Tests.cs))
- [X] Find the two non-repeating elements in an array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/TwoNonRepeatingNums.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/TwoNonRepeatingNums_Tests.cs))
- [X] Find two repeating elements in an array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/TwoRepeatingNums.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/TwoRepeatingNums_Tests.cs))
- [X] Set bits in all numbers from 1 to n ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SetBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SetBits_Tests.cs))
- [X] Swap without temp ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SwapWithoutTemp.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SwapWithoutTemp_Tests.cs))
- [X] Swap all odd and even bits ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SwapOddEvenBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SwapBits_Tests.cs))
- [X] Swap bits ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SwapBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SwapBits_Tests.cs))
- [X] Minimum or Maximum of two integers ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/MinMaxOfTwoIntegers.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/MinMaxOfTwoIntegers_Tests.cs))
- [X] Add two numbers using bitwise operators ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/AddTwoNumbers.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/AddTwoNumbers_Tests.cs))
- [X] Add 1 to a number ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/AddOne.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/AddOne_Tests.cs))
- [X] A Boolean Array Puzzle ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/BoolArrayPuzzle.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/BoolArrayPuzzle_Tests.cs))
- [X] Set bits in an (big) array ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SetBitsBigArray.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SetBitsBigArray_Tests.cs))
- [X] Next higher number with same number of set bits ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/NextHighNumberWithSameSetBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/NextHighNumberWithSameSetBits_Tests.cs))
- [X] Absolute value (abs) without branching ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/AbsValue.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/AbsValue_Tests.cs))
- [X] Reverse Bits of a Number ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/ReverseBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/ReverseBits_Tests.cs))
- [X] Next Power of 2 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/NextPowOfTwo.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/NextPowOfTwo_Tests.cs))
- [X] Check if a Number is Multiple of 3 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/IsMultipleOfThree.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/IsMultipleOfThree_Tests.cs))
- [X] Check if a number is multiple of 9 ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/IsMultipleOfNine.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/IsMultipleOfNine_Tests.cs))
- [ ] Find parity ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/ParityFinder.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/ParityFinder_Tests.cs))
- [ ] Maximum Subarray XOR ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/MaxSubArrayXOR.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/MaxSubArrayXOR_Tests.cs))
- [ ] Magic Number ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/MagicNumber.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/MagicNumber_Tests.cs))
- [ ] Sum of bit differences among all pairs ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/SumBitDiff.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/SumBitDiff_Tests.cs))
- [ ] Find Next Sparse Number ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/NextSparseNumber.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/NextSparseNumber_Tests.cs))
- [ ] Binary Subsets ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/BinarySubsets.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/BinarySubsets_Tests.cs))
- [ ] Calculate Logarithm (base 2 & 10) ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/CalcLogarithm.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/CalcLogarithm_Tests.cs))
- [ ] Check if word has a zero byte ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/CheckWordForZeroByte.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/CheckWordForZeroByte_Tests.cs))
- [ ] Draw line on monochrome screen Problem ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/DrawLine.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/DrawLine_Tests.cs))
- [ ] Flip Bits for longest 1 sequence ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/FlipBitForLongest1sSeq.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/FlipBitForLongest1sSeq_Tests.cs))
- [ ] GCD ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/GCD.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/GCD_Tests.cs))
- [ ] Modify sub bits ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/InsertBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/InsertBits_Tests.cs))
- [ ] Interleave ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/InterleaveBits.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/InterleaveBits_Tests.cs))
- [ ] Next small/large number ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/NextSmallLargeNumber.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/NextSmallLargeNumber_Tests.cs))
- [ ] Test if coordinate is inside 8x8 grid ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/TestCordInside8x8Grid.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/TestCordInside8x8Grid_Tests.cs))
- [ ] Toggle upper/lower case ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/ToggleCase.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/ToggleCase_Tests.cs))
- [ ] Modulus of division by power of two ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/BitAlgorithms/DivisionModulus.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/BitAlgorithms/DivisionModulus_Tests.cs))


## Geometry

- [ ] Convex hull ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Geometry/ConvexHull.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Geometry/ConvexHull_Tests.cs))
- [ ] Line intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Geometry/LineIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Geometry/LineIntersection_Tests.cs))
- [ ] Closest point pair ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Geometry/ClosestPointPair.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Geometry/ClosestPointPair_Tests.cs))
- [ ] Check if given point inside polygon ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Geometry/PointInsidePolygon.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Geometry/PointInsidePolygon_Tests.cs))
- [ ] Rectangle intersection ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/Geometry/RectIntersection.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/Geometry/RectIntersection_Tests.cs))

## Divide & Conquer

- [ ] Count Inversions ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DivideAndConquer/CountInversions.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DivideAndConquer/CountInversions_Tests.cs))
- [ ] Strassen’s Matrix Multiplication ([Implementation](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox/DivideAndConquer/StrassensMatrixMult.cs) | [Tests](https://github.com/justcoding121/Advanced-Algorithms/blob/master/Algorithm.Sandbox.Tests/DivideAndConquer/StrassensMatrixMult_Tests.cs))
 
