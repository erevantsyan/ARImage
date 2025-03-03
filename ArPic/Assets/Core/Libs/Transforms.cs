using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transforms
{

    #region Show/Hide/Toggles


    public static void Show(GameObject tar)
    {
        if (tar != null)
            if (tar.activeSelf == false) { tar.SetActive(true); }
    }
    public static void Show(Transform tar)
    {
        if (tar != null)
            if (tar.gameObject.activeSelf == false) { tar.gameObject.SetActive(true); }
    }
    public static void Show(Component tar)
    {
        if (tar != null)
            if (tar.gameObject.activeSelf == false) { tar.gameObject.SetActive(true); }
    }
    public static void Show(Component[] tars)
    {
        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
                if (tars[i].gameObject.activeSelf == false) { tars[i].gameObject.SetActive(true); }
        }
    }



    public static void Hide(GameObject tar)
    {
        if (tar != null)
            if (tar.activeSelf == true) { tar.SetActive(false); }
    }
    public static void Hide(Transform tar)
    {
        if (tar != null)
            if (tar.gameObject.activeSelf == true) { tar.gameObject.SetActive(false); }
    }
    public static void Hide(Component tar)
    {
        if (tar != null)
            if (tar.gameObject.activeSelf == true) { tar.gameObject.SetActive(false); }
    }
    public static void Hide(Component[] tars)
    {
        if (tars != null)
        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
                if (tars[i].gameObject.activeSelf == true) { tars[i].gameObject.SetActive(false); }
        }
    }
    public static void Toggle(Component tar)
    {
        if (tar != null)
            if (tar.gameObject.activeSelf == true) { tar.gameObject.SetActive(false); } else { tar.gameObject.SetActive(true); }
    }

    #endregion


    #region Clear/Destroy

    public static void Clear(GameObject tar)
    {
        Clear(tar.transform);
    }
    public static void Clear(Transform tar)
    {
        foreach (Transform child in tar)
        {
            Object.Destroy(child.gameObject);
        }
    }


    public static void Destroy(Transform tar)
    {
        Transforms.Destroy(tar.gameObject);
    }
    public static void Destroy(GameObject tar)
    {
        Clear(tar);
        Object.Destroy(tar);
    }

    public static void ClearColliders(Transform tar)
    {
        Collider[] colliders = tar.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            Object.Destroy(colliders[i]);
        }
    }

    public static void ClearRigitBody(Transform tar)
    {
        Rigidbody[] rigidbodies = tar.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            Object.Destroy(rigidbodies[i]);
        }
    }

    public static void DisableColliders(Transform tar)
    {
        int count = tar.childCount;
        for (int i = 0; i < count; i++)
        {
            DisableColliders(tar.GetChild(i));
        }
        if (tar.GetComponent<Collider>() != null)
        {
            tar.GetComponent<Collider>().enabled = false;
        }
    }

    #endregion


    #region Shaders

    public static Shader GetShander(GameObject tar, string tag = "")
    {
        Shader res = null;

        MeshRenderer[] tars = tar.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tag.Equals("") || tars[i].CompareTag(tag))
            {
                res = tars[i].material.shader;
            }
        }

        SkinnedMeshRenderer[] skintars = tar.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < skintars.Length; i++)
        {
            if (skintars.Equals("") || skintars[i].CompareTag(tag))
            {
                res = skintars[i].material.shader;
            }
        }

        return res;
    }

    public static void SetShader(Transform tar, Shader shader, string tag = "")
    {
        Renderer[] tars = tar.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null && tars[i].gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (tag.Equals("") || tars[i].CompareTag(tag))
                {
                    for (int j = 0; j < tars[i].materials.Length; j++)
                    {
                        tars[i].material.shader = shader;
                    }
                }
            }
        }

        SkinnedMeshRenderer[] skintars = tar.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < skintars.Length; i++)
        {
            if (skintars[i] != null && skintars[i].gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (skintars.Equals("") || skintars[i].CompareTag(tag))
                {
                    for (int j = 0; j < skintars[i].materials.Length; j++)
                    {
                        skintars[i].materials[j].shader = shader;
                    }
                }
            }
        }
    }
    public static void SetShader(GameObject tar, Shader shader, string tag = "")
    {
        SetShader(tar.transform, shader, tag);
    }

    #endregion


    #region GetLayer/Setlayer

    public static void SetLayer(Transform tar, LayerMask layer)
    {
        tar.gameObject.layer = layer;

        int count = tar.childCount;
        for (int i = 0; i < count; i++)
        {
            SetLayer(tar.GetChild(i), layer);
        }
    }
    public static void SetLayer(GameObject tar, LayerMask layer)
    {
        SetLayer(tar.transform, layer);
    }

    public static void SetLayer(Transform tar, string layer_name)
    {
        SetLayer(tar, LayerMask.NameToLayer(layer_name));
    }
    public static void SetLayer(GameObject tar, string layer_name)
    {
        SetLayer(tar.transform, LayerMask.NameToLayer(layer_name));
    }


    #endregion


    #region Meshes

    public static GameObject GetMeshObject(Transform tar, string tag = "")
    {
        GameObject res = null;

        MeshFilter[] tars = tar.GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
            {
                if (tag == "" || tars[i].CompareTag(tag))
                {
                    res = tars[i].gameObject;
                }
            }
        }

        Debug.Log(res.name);

        return res;
    }
    public static GameObject GetMeshObject(GameObject tar, string tag = "")
    {
        return GetMeshObject(tar.transform, tag);
    }

    #endregion


    #region GetBox/NormalScale/Normalize

    public struct Box
    {
        public Vector3 maxPoint;
        public Vector3 minPoint;
        public Vector3 midPoint;
        public Vector3 size;
    }

    static List<Vector3> FillVB(Transform trans)
    {
        List<Vector3> result = new List<Vector3>();

        MeshFilter mf = trans.GetComponent<MeshFilter>();

        if (mf != null)
        {
            if (mf.sharedMesh != null && mf.sharedMesh.vertices != null)
            {
                Vector3[] VB = mf.sharedMesh.vertices;

                Matrix4x4 m = Matrix4x4.TRS(trans.position, trans.rotation, trans.lossyScale);

                for (int i = 0; i < VB.Length; i++)
                {
                    result.Add(m.MultiplyPoint3x4(VB[i]));
                }
            }
        }

        for (int i = 0; i < trans.childCount; i++)
        {
            result.InsertRange(result.Count, FillVB(trans.GetChild(i)));
        }

        return result;
    }

    public static Box GetBox(GameObject obj)
    {
        Box result = new Box();

        List<Vector3> VB = new List<Vector3>();

        VB.InsertRange(VB.Count, FillVB(obj.transform));

        for (int i = 0; i < VB.Count; i++)
        {
            result.maxPoint = new Vector3(Mathf.Max(result.maxPoint.x, VB[i].x), Mathf.Max(result.maxPoint.y, VB[i].y), Mathf.Max(result.maxPoint.z, VB[i].z));
            result.minPoint = new Vector3(Mathf.Min(result.minPoint.x, VB[i].x), Mathf.Min(result.minPoint.y, VB[i].y), Mathf.Min(result.minPoint.z, VB[i].z));
        }

        result.midPoint = (result.maxPoint + result.minPoint) * 0.5f;
        result.size = result.maxPoint - result.minPoint;

        //float maxSize = Mathf.Max(prefSize.x, prefSize.y);
        //maxSize = Mathf.Max(maxSize, prefSize.z);

        return result;
    }

    public static void GetNormalScale(GameObject obj, out float NormalizeScale, out Vector3 NormalizeVector)
    {
        Box box = GetBox(obj);

        float maxSize = Mathf.Max(box.size.x, box.size.y);
        maxSize = Mathf.Max(maxSize, box.size.z);

        NormalizeScale = 1f / maxSize;
        NormalizeVector = new Vector3();
        NormalizeVector = -box.midPoint * NormalizeScale;
    }

    #endregion


    #region Scaled

    public static void ScaleTransformsByNames(GameObject root, string[] names, Vector3 new_scale)
    {
        Transform curr;
        Vector3 curr_scale;

        for (int i = 0; i < names.Length; i++)
        {
            curr = root.transform.FindGrandChild(names[i]);

            curr_scale = curr.transform.localScale;

            if (new_scale.x == 0f) { new_scale.x = curr_scale.x; }
            if (new_scale.y == 0f) { new_scale.y = curr_scale.y; }
            if (new_scale.z == 0f) { new_scale.z = curr_scale.z; }

            if (curr != null)
            {
                curr.transform.localScale = new_scale;
            }
        }
    }

    public static void ScaleNodesByNames(GameObject root, string[] names, Vector3 new_scale)
    {
        Transform curr;
        Vector3 curr_scale;

        Transform child;

        for (int i = 0; i < names.Length; i++)
        {
            curr = root.transform.FindGrandChild(names[i]);

            if (curr != null)
            {
                curr_scale = curr.transform.localScale;

                if (new_scale.x == 0f) { new_scale.x = curr_scale.x; }
                if (new_scale.y == 0f) { new_scale.y = curr_scale.y; }
                if (new_scale.z == 0f) { new_scale.z = curr_scale.z; }

                curr.transform.localScale = new_scale;

                for (int j = 0; j < curr.childCount; j++)
                {
                    child = curr.GetChild(j);
                    child.localScale = new Vector3(child.localScale.x * curr_scale.x / new_scale.x, child.localScale.y * curr_scale.y / new_scale.y, child.localScale.z * curr_scale.z / new_scale.z);
                }
            }
        }
    }

    #endregion


    #region Materials

    public static List<string> GetMaterialsNamesList(GameObject tar, bool shared = true)
    {
        List<string> materials = new List<string>();

        SkinnedMeshRenderer[] meshs = tar.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < meshs.Length; i++)
        {
            if (shared)
            {
                for (int j = 0; j < meshs[i].sharedMaterials.Length; j++)
                {
                    if (meshs[i].sharedMaterials[j] != null)
                    {
                        if (!materials.Contains(meshs[i].sharedMaterials[j].name))
                        {
                            materials.Add(meshs[i].sharedMaterials[j].name);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < meshs[i].materials.Length; j++)
                {
                    if (meshs[i].materials[j] != null)
                    {
                        if (!materials.Contains(meshs[i].materials[j].name))
                        {
                            materials.Add(meshs[i].materials[j].name);
                        }
                    }
                }
            }
        }

        return materials;
    }
    public static List<Material> GetMaterialsList(GameObject tar, bool shared = true)
    {
        List<Material> materials = new List<Material>();

        SkinnedMeshRenderer[] meshs = tar.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < meshs.Length; i++)
        {
            if (shared)
            {
                for (int j = 0; j < meshs[i].sharedMaterials.Length; j++)
                {
                    if (meshs[i].sharedMaterials[j] != null)
                    {
                        if (!materials.Contains(meshs[i].sharedMaterials[j]))
                        {
                            materials.Add(meshs[i].sharedMaterials[j]);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < meshs[i].materials.Length; j++)
                {
                    if (meshs[i].materials[j] != null)
                    {
                        if (!materials.Contains(meshs[i].materials[j]))
                        {
                            materials.Add(meshs[i].materials[j]);
                        }
                    }
                }
            }
        }

        return materials;
    }


    public static void SetMaterial(Transform tar, Material material, string tag = "")
    {
        //tar.GetComponent<Renderer>().material = material;

        Renderer[] tars = tar.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
            {
                if (tag == "" || tars[i].CompareTag(tag))
                {
                    tars[i].material = material;
                }
            }
        }
    }

    public static void ReplaceMaterial(Transform tar, Material new_material, string old_material)
    {
        //tar.GetComponent<Renderer>().material = material;

        Renderer[] tars = tar.GetComponentsInChildren<Renderer>();

        List<Material> materials = new List<Material>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
            {
                materials.Clear();

                for (int j = 0; j < tars[i].materials.Length; j++)
                {
                    if (tars[i].sharedMaterials[j].name == old_material + " (Instance)")
                    {
                        materials.Add(new_material);
                    }
                    else
                    {
                        materials.Add(tars[i].materials[j]);
                    }
                }

                tars[i].SetMaterials(materials);
            }
        }
    }

    #endregion

    #region Shapes

    public static float FindShapeValue(SkinnedMeshRenderer skin, string name)
    {
        float value = -1f;

        int i = skin.sharedMesh.GetBlendShapeIndex(name);

        if (i != -1)
        {
            value = skin.GetBlendShapeWeight(i);
        }

        return value;
    }

    public static bool SetShapeValue(SkinnedMeshRenderer skin, string name, float value)
    {
        bool res = false;

        int i = skin.sharedMesh.GetBlendShapeIndex(name);

        if (i != -1)
        {
            skin.SetBlendShapeWeight(i, value);
            res = true;
        }

        return res;
    }


    #endregion


    #region Finds

    public static Transform FindGrandChild(this Transform aParent, string aName)
    {
        var result = aParent.ChildContainsName(aName);

        if (result != null) return result;

        foreach (Transform child in aParent)
        {
            result = child.FindGrandChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static Transform ChildContainsName(this Transform aParent, string aName)
    {
        foreach (Transform child in aParent)
        {
            if (child.name.Contains(aName))
                return child;
        }
        return null;
    }

    #endregion


    public static GameObject GetFolder(string name, GameObject parent)
    {
        GameObject res;

        Transform folder = parent.transform.Find(name);

        if (folder != null)
        {
            res = folder.gameObject;
        }
        else
        {
            res = new UnityEngine.GameObject();

            res.name = name;
            res.transform.parent = parent.transform;
            res.transform.localPosition = Vector3.zero;
            res.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        return res;
    }
    public static Transform GetFolder(string name, Transform parent)
    {
        return GetFolder(name, parent.gameObject).transform;
    }


    public static GameObject CreateFolder(string name, GameObject parent)
    {
        GameObject res = new UnityEngine.GameObject();

        res.name = name;
        res.transform.parent = parent.transform;
        res.transform.localPosition = Vector3.zero;
        res.transform.localScale = new Vector3(1f, 1f, 1f);

        return res;
    }
    public static Transform CreateFolder(string name, Transform parent)
    {
        return CreateFolder(name, parent.gameObject).transform;
    }


    public static GameObject FindParentByTag(GameObject obj, string tag)
    {
        GameObject res = null;

        GameObject cur = obj;
        while (res == null && cur.transform.parent != null)
        {
            if (cur.CompareTag(tag))
            {
                res = cur;
            }

            cur = cur.transform.parent.gameObject;
        }

        return res;
    }


    public static GameObject GetChildByName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);

        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }


    public static GameObject GetRenderObjectByTag(Transform tar, string tag = "")
    {
        //tar.GetComponent<Renderer>().material = material;

        GameObject res = new GameObject();

        Renderer[] tars = tar.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < tars.Length; i++)
        {
            if (tars[i] != null)
            {
                if (tag == "" || tars[i].CompareTag(tag))
                {
                    res = tars[i].gameObject;
                }
            }
        }

        return res;
    }


    public static bool PlaneCheck(out Vector3 result, Vector3 position, Vector3 direction, Vector3 planePos)
    {
        bool intersect = false;
        result = Vector3.zero;

        float denominator = Vector3.Dot(direction, Vector3.down);

        if (denominator > 0.00001f)
        {
            //The distance to the plane
            float t = Vector3.Dot(planePos - position, Vector3.down) / denominator;

            //Where the ray intersects with a plane
            result = position + direction * t;

            intersect = true;
        }
        return intersect;
    }

}
